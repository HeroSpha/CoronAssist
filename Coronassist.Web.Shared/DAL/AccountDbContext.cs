using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.DAL
{
    public class AccountDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<AccountSurvey> AccountSurveys { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<SurveyAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<FlightDetail> FlightDetails { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                 .HasKey(f => f.Id);
          
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
