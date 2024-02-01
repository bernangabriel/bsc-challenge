using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BSC.Web.Infrastructure.Base
{
    public class ApiControllerBase : Controller
    {
        public Guid UserId
        {
            get
            {
                return HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier)
                    ? Guid.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value)
                    : Guid.Empty;
            }
        }

        public string UserName
        {
            get
            {
                return HttpContext.User.Claims.Any(x => x.Type == JwtRegisteredClaimNames.UniqueName)
                    ? HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value
                    : string.Empty;
            }
        }
    }
}