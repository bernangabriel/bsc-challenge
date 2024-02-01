namespace BSC.Business.Entities.DTOs;

public class UserDto
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
}