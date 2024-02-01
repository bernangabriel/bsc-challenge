using BSC.Core.Common.Contracts;
using BSC.Core.Common.Settings;
using BSC.Web.Infrastructure.Contracts;
using BSC.Web.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Web.Infrastructure.Installers
{
    public class CryptProviderInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var securitySetting = new SecuritySetting();
            configuration.GetSection(nameof(SecuritySetting)).Bind(securitySetting);

            if (services.All(x => x.ServiceType != typeof(SecuritySetting)))
                services.AddSingleton(securitySetting);

            services.AddScoped<ICryptProvider, CryptProvider>();
        }
    }
}