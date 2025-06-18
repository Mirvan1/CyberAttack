using CyberAttack.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CyberAttack.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ScanController : ControllerBase
{
    private static readonly HttpClient Http = new HttpClient(new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

    private readonly IConfiguration _configuration;

    public ScanController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpGet("active-scan")]
    public async Task<IActionResult> StartActiveScan([FromQuery] string targetUrl, [FromQuery] string attackTypes)
    {
        try
        {
            string _baseUrl = _configuration.GetValue<string>("baseUrl");
            string _apiKey = _configuration.GetValue<string>("apiKey");
             await Http.GetAsync($"{_baseUrl}/JSON/ascan/action/disableAllScanners?apikey={_apiKey}");
             await Http.GetAsync($"{_baseUrl}/JSON/core/action/accessUrl?apikey={_apiKey}&url={Uri.EscapeDataString(targetUrl)}&followRedirects=true");
            var spiderJson = await Http.GetStringAsync($"{_baseUrl}/JSON/spider/action/scan?apikey={_apiKey}&url={Uri.EscapeDataString(targetUrl)}&recurse=true");
            using var spDoc = JsonDocument.Parse(spiderJson);
            int spiderId = int.Parse(spDoc.RootElement.GetProperty("scan").GetString());
            int status;
           // do
           // {
                await Task.Delay(2000);
                var stat = await Http.GetStringAsync($"{_baseUrl}/JSON/spider/view/status?apikey={_apiKey}&scanId={spiderId}");
                using var sd = JsonDocument.Parse(stat);
                status = int.Parse(sd.RootElement.GetProperty("status").GetString());
            // } while (status < 100);
            if (status < 100) return Ok(new AttackResult() { Success = true, Status = status });
             if (!string.IsNullOrEmpty(attackTypes))
                await Http.GetAsync($"{_baseUrl}/JSON/ascan/action/enableScanners?apikey={_apiKey}&ids={attackTypes}");

             var scanJson = await Http.GetStringAsync($"{_baseUrl}/JSON/ascan/action/scan?apikey={_apiKey}&url={Uri.EscapeDataString(targetUrl)}&recurse=true&inScopeOnly=false");
            using var scDoc = JsonDocument.Parse(scanJson);
            int scanId = int.Parse(scDoc.RootElement.GetProperty("scan").GetString());

            return Ok(new AttackResult { Success = true, ScanId = scanId });
        }
        catch (Exception ex)
        {
            return BadRequest(new AttackResult { Success = false, ErrorMessage = ex.Message });
        }
    }


    [HttpGet("disable-scan")]
    public async Task<IActionResult> DisableScanner()
    {
        string _baseUrl = _configuration.GetValue<string>("baseUrl");
        string _apiKey = _configuration.GetValue<string>("apiKey");
        await Http.GetAsync($"{_baseUrl}/JSON/ascan/action/disableAllScanners?apikey={_apiKey}");
        return Ok();
    }
    [HttpGet("status-attack2")]
    public async Task<IActionResult> AttackStatus2(
    [FromQuery] int scanId,
    [FromQuery] string targetUrl,
    [FromQuery] string attackTypes)
    {
        try
        {
            string _baseUrl = _configuration.GetValue<string>("baseUrl");
            string _apiKey = _configuration.GetValue<string>("apiKey");
             var statusRes = await Http.GetStringAsync(
                $"{_baseUrl}/JSON/spider/view/status?apikey={_apiKey}&scanId={scanId}");
            using var statusDoc = JsonDocument.Parse(statusRes);
            var statusStr = statusDoc.RootElement.GetProperty("status").GetString();
            if (!int.TryParse(statusStr, out var statusVal))
                throw new InvalidOperationException("Invalid status format");

             if (statusVal < 100)
                return Ok(new StatusResult { Success = true, Status = statusVal });

             //var alertsRes = await Http.GetStringAsync(
            //    $"{_baseUrl}/JSON/ascan/view/alerts?apikey={_apiKey}"
            //    + $"&scanId={scanId}&start=0&count=100");
            //using var alertDoc = JsonDocument.Parse(alertsRes);

            var alertsRes = await Http.GetStringAsync(
$"{_baseUrl}/JSON/core/view/alerts?apikey={_apiKey}"+
$"&baseurl={Uri.EscapeDataString(targetUrl)}&start=0&count=100");
            using var alertDoc = JsonDocument.Parse(alertsRes);

            if (!alertDoc.RootElement.TryGetProperty("alerts", out var alertsArray))
                throw new KeyNotFoundException("'alerts' not found in ZAP response.");

             var filterSet = new HashSet<string>(
                attackTypes
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            );

            var list = new HashSet<Alert>();
            foreach (var item in alertsArray.EnumerateArray())
            {
                var pid = item.GetProperty("pluginId").GetString();
                if (filterSet.Count > 0 && !filterSet.Contains(pid))
                    continue;

                list.Add(new Alert
                {
                    PluginId = pid,
                    Name = item.GetProperty("alert").GetString(),
                    Risk = item.GetProperty("risk").GetString(),
                    Description = item.GetProperty("description").GetString(),
                    Solution = item.GetProperty("solution").GetString(),
                    Url = item.GetProperty("url").GetString()
                });
            }

            return Ok(new AlertExtResult { Success = true, Alerts = list.ToList(),Status=statusVal });
        }
        catch (Exception ex)
        {
            return BadRequest(new StatusResult { Success = false, ErrorMessage = ex.Message });
        }
    }

    [HttpGet("passive-scan")]
    public async Task<IActionResult> StartPassiveScan([FromQuery] string targetUrl, [FromQuery] string attackTypes)
    {
        try
        {
            string _baseUrl = _configuration.GetValue<string>("baseUrl");
            string _apiKey = _configuration.GetValue<string>("apiKey");
            await Http.GetAsync($"{_baseUrl}/JSON/pscan/action/disableAllScanners?apikey={_apiKey}");
             if (!string.IsNullOrEmpty(attackTypes))
                await Http.GetAsync($"{_baseUrl}/JSON/pscan/action/enableScanners?apikey={_apiKey}&ids={attackTypes}");

             await Http.GetAsync($"{_baseUrl}/JSON/core/action/accessUrl?apikey={_apiKey}&url={Uri.EscapeDataString(targetUrl)}&followRedirects=true");

             var alertsJson = await Http.GetStringAsync($"{_baseUrl}/JSON/core/view/alerts?apikey={_apiKey}&baseurl={Uri.EscapeDataString(targetUrl)}&start=0&count=100");
           // using var alertDoc = JsonDocument.Parse(alertsJson);
            //var arr = alertDoc.RootElement.GetProperty("alerts").EnumerateArray();

            //var arrayText = alertDoc.RootElement.GetProperty("alerts").GetRawText();

            //var alerts = JsonSerializer.Deserialize<List<Alert>>(arrayText);

            var response = JsonSerializer.Deserialize<AlertsResponse>(alertsJson);
            var alerts = response.Alerts;

             var filterSet = new HashSet<string>(attackTypes.Split(',', StringSplitOptions.RemoveEmptyEntries));
            var list = alerts
                .Where(item => filterSet.Contains(item.PluginId))
                .Select(item => new Alert
                {
                    PluginId = item.PluginId,
                    Name = item.Name,
                    Risk = item.Risk,
                    Description = item.Description,
                    Solution = item.Solution,
                    Url = item.Url
                })
                .ToList();

            return Ok(new AlertExtResult { Success = true, Alerts = list });
        }
        catch (Exception ex)
        {
            return BadRequest(new StatusResult { Success = false, ErrorMessage = ex.Message });
        }
    }


    [HttpGet("directory-brute-force")]
    public async Task<IActionResult> DirBruteForce([FromQuery] string targetUrl)
    {
        if (string.IsNullOrWhiteSpace(targetUrl))
            return BadRequest( new DirBruteForceResult() { Success=false,ErrorMessage="targetUrl is required" });

        var words = await System.IO.File.ReadAllLinesAsync("Utils/dirbrute.txt");
        var results = new ConcurrentBag<string>();
        var throttle = new SemaphoreSlim(20);
        var baselineResponse = await Http.GetAsync($"{targetUrl.TrimEnd('/')}");
        var baselineContent = await baselineResponse.Content.ReadAsStringAsync();
        int baselineLength = baselineContent.Length;



        var tasks = words.Take(100).Select(async word =>
        {
            await throttle.WaitAsync();
            try
            {
                var url = $"{targetUrl.TrimEnd('/')}/{word}";
                using var resp = await Http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                if ((resp.StatusCode == HttpStatusCode.MovedPermanently ||
     resp.StatusCode == HttpStatusCode.Found) &&
    resp.Headers.Location != null &&
    resp.Headers.Location.ToString().TrimEnd('/') == targetUrl.TrimEnd('/')) { 
                
                    return;
                }

                var body = await resp.Content.ReadAsStringAsync();

                if (body.Length == baselineLength)
                {
                    return;
                }


                if (resp.StatusCode != HttpStatusCode.NotFound)
                {

                    results.Add($"{url} - {(int)resp.StatusCode}");
                }
            }
            finally
            {
                throttle.Release();
            }
        });
        await Task.WhenAll(tasks);

        return Ok(new DirBruteForceResult { Success = true, UrlLeaks = results.ToList()});


    }





    [HttpGet("ssl-report")]
    public async Task<IActionResult> GetSslReport([FromQuery] string targetUrl)
    {
        if (!Uri.TryCreate(targetUrl, UriKind.Absolute, out var uri))
            return BadRequest("Invalid URL");

        var host = uri.Host;
        var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        var analyzeUrl = $"https://api.ssllabs.com/api/v3/analyze?host={host}&all=done&fromCache=false";
        var analyzeRes = await client.GetAsync(analyzeUrl);
        if (!analyzeRes.IsSuccessStatusCode)
            return StatusCode((int)analyzeRes.StatusCode, "SSL Labs API error");

        SslLabsStatus status;
        do
        {
            await Task.Delay(5000);
            var statusRes = await client.GetStringAsync($"https://api.ssllabs.com/api/v3/analyze?host={host}");
            status = JsonSerializer.Deserialize<SslLabsStatus>(statusRes)!;
        } while (status.status != "READY" && status.status != "ERROR");

       if (status.status == "ERROR")
            return BadRequest("SSL Labs analysis failed: " + status.statusMessage);

         var reportJson = await client.GetStringAsync($"https://api.ssllabs.com/api/v3/analyze?host={host}&all=done&fromCache=true");
        var report = JsonSerializer.Deserialize<SslLabsReport>(reportJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        var summary = new SslSummary
        {
            Host = report.host,
            Status = report.status,
            TestTime = DateTimeOffset.FromUnixTimeMilliseconds(report.testTime).UtcDateTime,
            Certificate = new CertificateSummary
            {
                CommonNames = report.certs.First().commonNames,
                NotBefore = DateTimeOffset.FromUnixTimeMilliseconds(report.certs.First().notBefore).UtcDateTime,
                NotAfter = DateTimeOffset.FromUnixTimeMilliseconds(report.certs.First().notAfter).UtcDateTime
            },
            HstsPolicy = report.endpoints[0].details.hstsPolicy.status,
            Endpoints = report.endpoints.Select(e => new EndpointSummary
            {
                IpAddress = e.ipAddress,
                Grade = e.grade,
                HasWarnings = e.hasWarnings,
                Protocols = e.details.protocols
                                .Select(p => $"{p.name} {p.version}")
                                .ToList(),
                PreferredCiphers = e.details.suites
                                    .Where(s => s.preference)
                                    .SelectMany(s => s.list)
                                    .Select(c => c.name)
                                    .ToList()
            }).ToList()
        };

        return Ok(new SSLAnalyzerResult() { Success=true,sslSummary=summary});
    }
}




