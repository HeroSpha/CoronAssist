using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Settings
{
    public partial class SettingCard
    {
        public string Address { get; set; }
        private void UpdateSettings()
        {
            if(!string.IsNullOrEmpty(Address))
            {

            }
        }
    }
}