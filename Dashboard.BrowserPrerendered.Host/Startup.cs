using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Server;
using Skclusive.Blazor.Dashboard.App.View;
using Skclusive.Material.Layout;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Coronassist.Web.Shared.Models;
using Coronassist.Web.Shared.DAL;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.DAL.Core.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Skclusive.Blazor.Dashboard.BrowserPrerendered.Host
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
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddScoped<HttpClient>(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    BaseAddress = new Uri(navigationManager.BaseUri)
                };
            });
               services
                .AddDbContext<AccountDbContext>(options =>
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

            services.AddTransient<DatabaseService>(Factory);
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAccountSurveyRepository, AccountSurveyRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddTransient<IApplicationUserRepository, UserApplicationRepository>();
           // services.AddScoped<ISweetAlertMessage, SweetAlertMessage>();
            services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
            services.AddDashboardView(new LayoutConfigBuilder().WithResponsive(true).Build());
        }
        private DatabaseService Factory(IServiceProvider arg)
        {
            var con = Configuration["ConnectionString"];
            var optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
            optionsBuilder.UseSqlServer(con);
            return new DatabaseService(optionsBuilder.Options);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        // Use exceptionHandlerPathFeature to process the exception (for example, 
                        // logging), but do NOT expose sensitive error information directly to 
                        // the client.

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }

            app.UseClientSideBlazorFiles<Skclusive.Blazor.Dashboard.Browser.Host.Program>();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
