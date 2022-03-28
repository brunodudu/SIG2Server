namespace SIG2Server.Models;

public class SoftwareOM
{
    public long Id { get; set; }
    public long OMId { get; set; }
    public OM OM { get; set; }
    public long SoftwareId { get; set; }
    public Software Software { get; set; }
    public string Ip { get; set; }
}