using System.Collections.Generic;
using Skclusive.Material.Core;
using ChartJs.Blazor.Charts;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.Common;
using ChartJs.Blazor.ChartJS.BarChart.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using Microsoft.AspNetCore.Components;
using Coronassist.Web.Shared.DAL.Core.Interfaces;

namespace Skclusive.Blazor.Dashboard.App.View.Home
{
    public class LatestSalesComponent : MaterialComponent
    {
        [Inject]
        public IAccountSurveyRepository AccountSurveyRepository { get; set; }
        public LatestSalesComponent() : base("dashboard-sales")
        {
        }

        protected BarConfig _config;

        protected ChartJsBarChart _barChartJs;

        protected async override void OnInitialized()
        {
            _config = await AccountSurveyRepository.GetDailyReports();
            if (_config == null)
            {
                _config = new BarConfig();
            }
            //_config = new BarConfig();
            StateHasChanged();
        }
    }
}
