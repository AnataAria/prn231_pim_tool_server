namespace Service.DTO;
public class ProjectBaseResponse
{
    public long? GroupId { get; set; }
    public long? ProjectNumber { get; set; }
    public string? Name { get; set; }
    public string? Customer { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? Version { get; set; }
}
public class ProjectRequest
{
    public long? GroupId { get; set; }
    public string? Name { get; set; }
    public int ProjectNumber {get; set; }
    public string? Customer { get; set; }
    public string? Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}