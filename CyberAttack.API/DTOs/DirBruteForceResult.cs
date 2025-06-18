namespace CyberAttack.API.DTOs;

public class DirBruteForceResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public List<string> UrlLeaks { get; set; }
}
