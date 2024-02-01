using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BSC.Business.Engines.Contracts;
using BSC.Business.Entities.DTOs;
using BSC.Core.Common.Contracts;
using BSC.Web.Infrastructure.Base;
using BSC.Web.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BSC.Web.Api.Controllers;

[Route("api/account")]
[Authorize]
public class AccountApiController : ApiControllerBase
{
    private readonly IBusinessEngineFactory _businessEngineFactory;
    private readonly JwtSetting _jwtSetting;

    public AccountApiController(IBusinessEngineFactory businessEngineFactory, JwtSetting jwtSetting)
    {
        _businessEngineFactory = businessEngineFactory;
        _jwtSetting = jwtSetting;
    }

    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<SignInResultDto> Signin([FromBody] SignInInputDto input)
    {
        var user = await _businessEngineFactory.GetBusinessEngine<IMainEngine>()
            .SignInAsync(input.UserName, input.Password);

        if (user == null)
        {
            return new SignInResultDto
            {
                IsValid = false,
                Message = "User was not found."
            };
        }

        var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = _jwtSetting.Issuer,
            Audience = _jwtSetting.Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return new SignInResultDto
        {
            IsValid = true,
            AccessToken = jwtToken
        };
    }

    [HttpGet("user-profile-information")]
    public async Task<UserProfileInfoDto> GetUserProfile()
    {
        return await _businessEngineFactory.GetBusinessEngine<IMainEngine>()
            .GetUserProfileInformationAsync(UserId);
    }
}