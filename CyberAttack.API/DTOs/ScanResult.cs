namespace CyberAttack.API.DTOs;

public class ScanResult
{
    public string PluginId { get; set; }
    public string Name { get; set; }
    public string Risk { get; set; }
    public string Description { get; set; }
    public string Solution { get; set; }
    public string Url { get; set; }
}
