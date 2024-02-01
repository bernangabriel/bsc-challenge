using BSC.Business;
using BSC.Core.Common.Contracts;
using BSC.Web.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Web.Infrastructure.Installers
{
    public class EngineInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBusinessEngineFactory, BusinessEngineFactory>();

            var engineTypes =
                typeof(BusinessEngineFactory).Assembly
                    .ExportedTypes
                    .Where(x => typeof(IBusinessEngine).IsAssignableFrom(x) &&
                                !x.IsInterface &&
                                !x.IsAbstract).ToList();

            engineTypes.ForEach(engineType =>
            {
                services.AddScoped(engineType.GetInterface($"I{engineType.Name}"), engineType);
            });
        }
    }
}