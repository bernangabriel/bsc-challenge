namespace BSC.Business.Entities.DTOs;

public class UserProfileInfoDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
}