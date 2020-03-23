using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Skclusive.Material.Core;
using ChartJs.Blazor.Charts;
using ChartJs.Blazor.ChartJS.PieChart;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Enums;
using Skclusive.Material.Icon;
using Microsoft.AspNetCore.Components.Rendering;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Home
{
    public class UsersByDeviceComponent : MaterialComponent
    {
        [Inject]
        public IAccountSurveyRepository AccountSurveyRepository { get; set; }
        public UsersByDeviceComponent() : base("dashboard-userdivces")
        {
        }

        protected static List<(string title,  string color)> Devices = new List<(string title,  string color)>
        {
            (
                title: "Low",
                
                color: "#3f51b5"
            ),
            (
                title: "Mid",
               
                color: "#e53935"
            ),
            (
                title: "High",
              
                color: "#fb8c00"
            )
        };

        protected PieConfig _config;

        protected ChartJsPieChart _doughnutChartJs;
        protected async override Task OnInitializedAsync()
        {
             var data = await AccountSurveyRepository.GetDoughartData();

            _config = new PieConfig
            {
                Options = new PieOptions(true)
                {
                    Legend = new Legend
                    {
                        Display = false
                    },

                    Responsive = true,

                    MaintainAspectRatio = false,

                    CutoutPercentage = 80,

                    Tooltips = new Tooltips
                    {
                        Enabled = true,

                        Mode = InteractionMode.Index,

                        Intersect = false,

                        BorderWidth = 1,

                        BorderColor = "rgba(0, 0, 0, 0.12)",

                        BackgroundColor = "#ffffff",

                        TitleFontColor = "rgba(0, 0, 0, 0.87)",

                        BodyFontColor = "rgba(0, 0, 0, 0.54)",

                        FooterFontColor = "rgba(0, 0, 0, 0.54)"
                    }
                }
            };

            _config.Data.Labels.AddRange(new[] { "Low", "Medium", "High" });

            var doughnutSet = new PieDataset
            {
                BackgroundColor = new[] { "#0f812e", "#f4be14", "#FF0000" },

                BorderWidth = 8,

                HoverBorderColor = "#ffffff",

                BorderColor = "#ffffff"
            };

            doughnutSet.Data.AddRange(new double[] { 63, 15, 22 });

            _config.Data.Datasets.Add(doughnutSet);
            StateHasChanged();
        }
        
    }
}
