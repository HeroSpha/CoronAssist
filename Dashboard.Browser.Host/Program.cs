using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Blazor.Hosting;
using Skclusive.Blazor.Dashboard.App.View;
using Skclusive.Material.Layout;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.DAL.Core.Repositories;
using Coronassist.Web.Shared.DAL;
using Microsoft.EntityFrameworkCore;

namespace Skclusive.Blazor.Dashboard.Browser.Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);


            builder.RootComponents.Add<DashboardView>("app");

            builder.Services.AddDashboardView(new LayoutConfigBuilder().WithResponsive(true).Build());

            await builder.Build().RunAsync();
        }
        
    }
}
