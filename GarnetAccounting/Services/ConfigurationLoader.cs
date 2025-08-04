using GarnetAccounting.Interfaces;
using GarnetAccounting.Models;

namespace GarnetAccounting.Services
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private AppSettings _config;
        private readonly IServiceScopeFactory _scopFactory;
        public ConfigurationLoader(IServiceScopeFactory factory)
        {
            _scopFactory = factory;
            _config = LoadConfigurations();
        }

        private AppSettings LoadConfigurations()
        {
            using (var scope = _scopFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                return _db.AppSettings.FirstOrDefault();
            }
        }

        public AppSettings GetConfigurations()
        {
            return _config;
        }


    }
}
