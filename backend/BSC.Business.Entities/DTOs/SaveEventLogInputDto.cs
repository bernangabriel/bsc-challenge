using BSC.Business.Entities.Enums;

namespace BSC.Business.Entities.DTOs;

public class SaveEventLogInputDto
{
    public Guid UserId { get; set; }
    public EventTypes EventTypeId { get; set; }
    public string Payload { get; set; }
}