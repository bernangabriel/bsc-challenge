namespace BSC.Business.Entities.DTOs;

public class SignInResultDto
{
   public bool IsValid { get; set; }
   public string Message { get; set; }
   public string AccessToken { get; set; }
}