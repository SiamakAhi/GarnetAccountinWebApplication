using GarnetAccounting.Interfaces;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Services
{
    public class UISettingsService : IUISettingsService
    {
        private readonly AppDbContext _db;
        public UISettingsService(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public async Task<AppTheme> ThemeTogglerAsync(int id)
        {
            var theme = await _db.AppThemes.FirstOrDefaultAsync(n => n.Id == id);
            if (theme == null) return new();
            return theme;

        }
    }
}
