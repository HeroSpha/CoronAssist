using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface ISettingRepository
    {
        public Settings Settings { get; set; }
        Task<Settings> AddSettings(Settings settings);
        Task<Settings> GetSettings();
    }
}
