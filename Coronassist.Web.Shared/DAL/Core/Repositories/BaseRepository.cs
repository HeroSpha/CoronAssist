using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class BaseRepository
    {
        public AccountDbContext accountDbContext { get; set; }
        public BaseRepository(DbContextOptions<AccountDbContext> options)
        {
            accountDbContext  = new AccountDbContext(options);
        }
    }
}
