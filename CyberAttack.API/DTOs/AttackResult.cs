namespace CyberAttack.API.DTOs;

public class AttackResult
{
    public int ScanId { get; set; }
    public bool Success {  get; set; }
    public string ErrorMessage {  get; set; }
    public int Status { get; set; }
}
