using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Settings
    {
        public int SettingsId { get; set; }
        public decimal ConsultationFee { get; set; }
        public string Address { get; set; }
        public string OpenTime { get; set; }
        public string ClosingTime { get; set; }
        public bool OpenWeekend { get; set; }
    }
}
