using GarnetAccounting.Models;

namespace GarnetAccounting.Interfaces
{
    public interface IUISettingsService
    {
        Task<AppTheme> ThemeTogglerAsync(int id);
    }
}
