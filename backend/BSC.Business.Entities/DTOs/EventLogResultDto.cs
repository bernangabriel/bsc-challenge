namespace BSC.Business.Entities.DTOs;

public class EventLogResultDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string EventTypeName { get; set; }
    public DateTime EventDate { get; set; }
    public string Payload { get; set; }
}