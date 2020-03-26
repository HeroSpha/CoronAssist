using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class SettingRepository : BaseRepository, ISettingRepository
    {
        public SettingRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public Settings Settings { get; set ; }

        public async Task<Settings> AddSettings(Settings settings)
        {
            try
            {
                var _setting = await accountDbContext.Settings.FirstOrDefaultAsync(p => p.SettingsId == settings.SettingsId);
                if(_setting != null)
                {
                    _setting.OpenWeekend = settings.OpenWeekend;
                    _setting.ClosingTime = settings.ClosingTime;
                    _setting.ConsultationFee = settings.ConsultationFee;
                    _setting.Address = settings.Address;
                    _setting.OpenTime = settings.OpenTime;
                    accountDbContext.Settings.Update(_setting);
                    await accountDbContext.SaveChangesAsync();
                    return _setting;
                }
                else
                {
                    var setting = await accountDbContext.Settings.AddAsync(settings);
                    await accountDbContext.SaveChangesAsync();
                    return setting.Entity;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Settings> GetSettings()
        {
            return await accountDbContext.Settings.FirstOrDefaultAsync();
        }
    }
}
