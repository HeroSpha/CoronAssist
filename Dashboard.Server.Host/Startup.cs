using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.DAL.Core.Repositories;
using Coronassist.Web.Shared.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Skclusive.Blazor.Dashboard.App.View;
using Skclusive.Blazor.Dashboard.App.View.AlertService;
using Skclusive.Material.Layout;

namespace Skclusive.Blazor.Dashboard.Server.Host
{
    public class Startup
    {
        public static byte[] SecretKey = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);
            services.AddDbContext<AccountDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"]);
            });


            //login in
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 5;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            })
           .AddEntityFrameworkStores<AccountDbContext>()
           .AddDefaultTokenProviders();

            //var appSettingsSection = Configuration.GetSection("Jwt");
            //services.Configure<AppSettings>(appSettingsSection);
            //var appSettings = appSettingsSection.Get<AppSettings>();
            SecretKey = Encoding.ASCII.GetBytes(Configuration["SigningKey"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = true;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = Configuration["Site"],
                   ValidIssuer = Configuration["Site"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Site"]))
               };
           });


            //services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddHttpClient();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllersWithViews();
            services.AddDashboardView(new LayoutConfigBuilder().WithResponsive(true).Build());
            // services.AddTransient<DatabaseService>(Factory);
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAccountSurveyRepository, AccountSurveyRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IApplicationUserRepository, UserApplicationRepository>();
            services.AddScoped<ISweetAlertMessage, SweetAlertMessage>();
            services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<IFlightDetailRepository, FlightDetailRepository>();
            //services.AddScoped<SweetAlertService>();
            //services.AddSweetAlert2();
        }
        //private DatabaseService Factory(IServiceProvider arg)
        //{
        //    var con = Configuration["ConnectionString"];
        //    var optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
        //    optionsBuilder.UseSqlServer(con);
        //    return new DatabaseService(optionsBuilder.Options);
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
