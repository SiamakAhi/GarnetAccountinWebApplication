using GarnetAccounting.Models;

namespace GarnetAccounting.Interfaces
{
    public interface IConfigurationLoader
    {
        AppSettings GetConfigurations();
    }
}
