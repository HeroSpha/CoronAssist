using Coronassist.Web.Shared.Models;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.BarChart.Axes;
using ChartJs.Blazor.ChartJS.Common;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class AccountSurveyRepository : BaseRepository, IAccountSurveyRepository
    {
        public AccountSurveyRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<AccountSurvey> AddAccountSurvey(AccountSurvey accountSurvey)
        {
            try
            {
                var _add = await accountDbContext.AccountSurveys.AddAsync(accountSurvey);
                await accountDbContext.SaveChangesAsync();
                return _add.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AccountSurvey> GetAccountSurvey(int accountSurveyId)
        {
            try
            {
                var _accountSurvey = await accountDbContext
                    .AccountSurveys.Include(f => f.Survey)
                    .Include(f => f.Account)
                    .FirstOrDefaultAsync(p => p.AccountSurveyId == accountSurveyId);
                return new AccountSurvey
                {
                    AccountSurveyId = _accountSurvey.AccountSurveyId,
                    Id = _accountSurvey.Id,
                    Risk = _accountSurvey.Risk,
                    SurveyDate = _accountSurvey.SurveyDate,
                    SurveyId = _accountSurvey.SurveyId,
                    UserSurveyStatus = _accountSurvey.UserSurveyStatus,
                    Account = new ApplicationUser
                    {
                        FullAddress = _accountSurvey.Account.FullAddress,
                        PhoneNumber = _accountSurvey.Account.PhoneNumber,
                        Email = _accountSurvey.Account.Email,
                        Fullname = _accountSurvey.Account.Fullname,
                        Province = _accountSurvey.Account.Province
                    },
                    Survey = new Survey
                    {
                        Name = _accountSurvey.Survey.Name
                    }
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<BarConfig> GetDailyReports()
        {
            var BarConfig = new BarConfig();
            BarConfig.Options = new BarOptions
            {
                Legend = new Legend
                {
                    Display = false
                },

                Responsive = true,

                MaintainAspectRatio = false,

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
                },

                Scales = new BarScales
                {
                    XAxes = new List<CartesianAxis>
                        {
                            new BarCategoryAxis
                            {
                                BarThickness = 12,

                                MaxBarThickness = 10,

                                BarPercentage = 0.5,

                                CategoryPercentage= 0.5,

                                // Ticks = new CategoryTicks
                                // {
                                //     FontColor = "rgba(0, 0, 0, 0.54)"
                                // },

                                GridLines = new GridLines
                                {
                                    Display = false,

                                    DrawBorder = false,

                                    OffsetGridLines = true
                                },

                                Offset = true,

                                OffsetGridLines = true
                            }
                        },

                    YAxes = new List<CartesianAxis>
                        {
                            new BarLinearCartesianAxis
                            {
                                BarThickness = 12,

                                MaxBarThickness = 10,

                                BarPercentage = 0.5,

                                CategoryPercentage= 0.5,

                                Ticks = new LinearCartesianTicks {
                                    BeginAtZero = true,

                                    Min = 0

                                    // FontColor = "rgba(0, 0, 0, 0.54)"
							    },

                                GridLines = new GridLines
                                {
                                    BorderDash = new double [] { 2 },

                                    DrawBorder = false,

                                    Color = "rgba(0, 0, 0, 0.12)",

                                    ZeroLineBorderDash = new int [] { 2 },

                                    ZeroLineBorderDashOffset = 2,

                                    ZeroLineColor = "rgba(0, 0, 0, 0.12)"
                                }
                            }
                        }
                }
            };
            //Get grouped surveys by dates
            var bars = new List<BarDataset<DoubleWrapper>>();
            var reports = await accountDbContext.AccountSurveys
                .ToListAsync();
           
            //Grouped by dates
            var groupedDates = reports.OrderBy(p => p.SurveyDate)
                .GroupBy(p => p.SurveyDate.ToString("MMMM dd"))
                .Select(g => new
                {
                    g.Key,
                    UserSurveys = g.Select(survey => new AccountSurvey
                    {
                        UserSurveyStatus = survey.UserSurveyStatus,
                        SurveyDate = survey.SurveyDate
                    })
                });
            var lowBarSet = new BarDataset<DoubleWrapper>
            {
                Label = "Low",

                BackgroundColor = "#0f812e"
            };
            var midBarSet = new BarDataset<DoubleWrapper>
            {
                Label = "Mid",

                BackgroundColor = "#f4be14"
            };
            var HighBarSet = new BarDataset<DoubleWrapper>
            {
                Label = "High",

                BackgroundColor = "#FF0000"
            };
            //Creates bottom labels by using dates
            foreach (var groupDate in groupedDates)
            {
                //Add data label
                BarConfig.Data.Labels.Add(groupDate.Key);

                var _survey = groupDate.UserSurveys
                .OrderByDescending(x => x.UserSurveyStatus)
                .GroupBy(x => x.UserSurveyStatus)
                .Select(g => new
                {
                    Key = g.Key,
                    Items = g.Select(item => new AccountSurvey
                    {
                        UserSurveyStatus = item.UserSurveyStatus,
                        AccountSurveyId = item.AccountSurveyId
                    })
                });

                lowBarSet.Add(_survey.FirstOrDefault(p => p.Key == UserSurveyStatus.Low) == null ? 0 : _survey.FirstOrDefault(p => p.Key == UserSurveyStatus.Low).Items.Count());
                midBarSet.Add(_survey.FirstOrDefault(p => p.Key == UserSurveyStatus.Midium) == null ? 0 :_survey.FirstOrDefault(p => p.Key == UserSurveyStatus.Midium).Items.Count());
                HighBarSet.Add(_survey.FirstOrDefault(p => p.Key == UserSurveyStatus.High) == null ? 0 :_survey.FirstOrDefault(p => p.Key == UserSurveyStatus.High).Items.Count());



            }
            BarConfig.Data.Datasets.AddRange(new[] { lowBarSet, midBarSet, HighBarSet });
            return BarConfig;
        }

        public async Task<(double low, double mid, double high)> GetDoughartData()
        {
            //var reports = await accountDbContext.AccountSurveys.Where(p => p.SurveyDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToListAsync();

            var reports = new List<AccountSurvey>()
            {
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-10), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-9), UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-8), UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-8), UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-8), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-5), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-1), UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-3), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-4), UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                  new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-8), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-1), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-7), UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-7), UserSurveyStatus = UserSurveyStatus.Midium},
                new AccountSurvey { SurveyDate = DateTime.Now.AddDays(-6), UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.Low},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High},
                new AccountSurvey { SurveyDate = DateTime.Now, UserSurveyStatus = UserSurveyStatus.High}


            };
            var groupedStatuses = reports.GroupBy(p => p.UserSurveyStatus)
                .Select(g => new
                {
                    g.Key,
                    Surveys = g.Select(survey => new AccountSurvey
                    {
                        UserSurveyStatus = survey.UserSurveyStatus
                    })
                });
            var low = groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.Low) == null ? 0 : groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.Low).Surveys.Count();
            var mid = groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.Midium) == null ? 0 : groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.Midium).Surveys.Count();
            var high = groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.High) == null ? 0 : groupedStatuses.FirstOrDefault(p => p.Key == UserSurveyStatus.High).Surveys.Count();
            return (low, mid, high);
        }

        string GetColorScheme(UserSurveyStatus userSurveyStatus)
        {
            var color = "";
            switch(userSurveyStatus)
            {
                case UserSurveyStatus.High:
                    {
                        color = "#FF0000";
                    }
                    break;
                case UserSurveyStatus.Low:
                    {
                        color = "#0f812e";
                    }
                    break;
                case UserSurveyStatus.Midium:
                    {
                        color = "#f4be14";
                    }
                    break;
            }
            return color;
        }
       
    }
}
