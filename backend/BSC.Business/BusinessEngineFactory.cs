using BSC.Core.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Business
{
    /// <summary>
    /// Importable class that represents a factory of Business Engines
    /// </summary>
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        private readonly IServiceProvider _services;

        public BusinessEngineFactory(IServiceProvider services)
        {
            _services = services;
        }

        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            return _services.GetRequiredService<T>();
        }
    }
}