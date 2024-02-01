using BSC.Web.Infrastructure.Contracts;
using Microsoft.OpenApi.Models;

namespace BSC.Web.Api.Infrastructure.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "BSC", Version = "v1" }); });
        }
    }
}