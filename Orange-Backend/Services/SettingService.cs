using Microsoft.EntityFrameworkCore;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Orange_Backend.Services.Interfaces;
using System.Configuration;

namespace Orange_Backend.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.Where(m => !m.SoftDeleted)
                                         .AsEnumerable()
                                         .ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
