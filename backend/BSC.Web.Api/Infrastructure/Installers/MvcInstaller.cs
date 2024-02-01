using System.Globalization;
using BSC.Web.Infrastructure.Contracts;

namespace BSC.Web.Api.Infrastructure.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Antiforgery Setup
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

            //Add Culture
            var cultureInfo = new CultureInfo("es-DO");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllers();

            services.AddOptions();

            services.AddDistributedMemoryCache();

            //Authentication
            // services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //     .AddIdentityServerAuthentication(options =>
            //     {
            //         options.Authority = authorizerSetting.Authority;
            //         options.ApiName = authorizerSetting.ApiName;
            //         options.ApiSecret = authorizerSetting.ApiSecret;
            //         options.EnableCaching = true;
            //         options.CacheDuration = TimeSpan.FromMinutes(authorizerSetting.CacheDuration);
            //         options.SupportedTokens = SupportedTokens.Jwt;
            //         options.RequireHttpsMetadata = false;
            //
            //         options.JwtBearerEvents.OnTokenValidated = async (context) =>
            //         {
            //             var identity = context.Principal.Identity as ClaimsIdentity;
            //
            //             var currentUserIdClaim = identity.Claims.FirstOrDefault(x => x.Type == "sub");
            //             var currentRoleIdClaim = identity.Claims.FirstOrDefault(x => x.Type == "roleid");
            //
            //             if(currentUserIdClaim == null)
            //                 return;
            //
            //             // load user specific data from database
            //             var engineFactory = context.HttpContext.RequestServices.GetRequiredService<IBusinessEngineFactory>();
            //             var profileEngine = engineFactory.GetBusinessEngine<IProfileEngine>();
            //
            //             var profileData = await profileEngine.GetUserProfileAdditionalClaimsAsync(Guid.Parse(currentUserIdClaim.Value), Guid.Parse(currentRoleIdClaim.Value));
            //
            //             var claims = new List<Claim>();
            //
            //             foreach(var item in profileData)
            //             {
            //                 claims.Add(new Claim(item.Id, item.Value));
            //             }
            //
            //             // add claims to the identity
            //             identity.AddClaims(claims);
            //         };
            //
            //     });
            
            // services.AddMvcCore()
            //     .AddAuthorization(options =>
            //     {
            //         options.DefaultPolicy = ScopePolicy.Create("digitalsignatureapi.fullaccess");
            //         options.AddPolicy("external", p =>
            //         {
            //             p.RequireScope("digitalsignatureapi.externalaccess");
            //         });
            //     })
            //     .AddApiExplorer();
        }
    }
}