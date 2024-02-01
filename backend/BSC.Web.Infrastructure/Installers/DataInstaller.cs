using BSC.Data;
using BSC.Web.Infrastructure.Contracts;
using BSC.Web.Infrastructure.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Web.Infrastructure.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = new ConnectionStrings();
            configuration.GetSection(nameof(ConnectionStrings)).Bind(connectionStrings);

            if (services.All(x => x.ServiceType != typeof(ConnectionStrings)))
                services.AddSingleton(connectionStrings);

            services.AddDbContext<BscDataContext>(options =>
            {
                var connection = new SqlConnection(connectionStrings.MainConnection);

                options.UseSqlServer(connection, (opt) => { opt.CommandTimeout(300); });
            });
        }
    }
}