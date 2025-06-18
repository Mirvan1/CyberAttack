using System.Text.Json.Serialization;

namespace CyberAttack.API.DTOs;

public class AlertExtResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public List<Alert > Alerts { get; set; }  
    public int Status { get; set; }

}




public class Alert
{
    [JsonPropertyName("pluginId")]
    public string PluginId { get; set; }

    // JSON'da "alert" alanı aslında bizim Name özelliğine geliyor
    [JsonPropertyName("alert")]
    public string Name { get; set; }

    [JsonPropertyName("risk")]
    public string Risk { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("solution")]
    public string Solution { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}


public class AlertsResponse
{
    [JsonPropertyName("alerts")]
    public List<Alert> Alerts { get; set; }
}
