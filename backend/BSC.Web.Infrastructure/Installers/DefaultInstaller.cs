using BSC.Core.Common.Contracts;
using BSC.Web.Infrastructure.Contracts;
using BSC.Web.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Web.Infrastructure.Installers
{
    public class DefaultInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = new JwtSetting();

            configuration.GetSection(nameof(JwtSetting)).Bind(jwtSetting);

            services.AddSingleton<JwtSetting>(jwtSetting);
        }
    }
}