using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.DAL
{
    public class DatabaseService : IDisposable
    {
        public readonly AccountDbContext accountContext;
        Boolean _disposeValue = false;
        public DatabaseService(DbContextOptions<AccountDbContext> options)
        {
            accountContext = new AccountDbContext(options);
        }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposeValue)
            {
                accountContext.Dispose();
                _disposeValue = true;
            }
        }
    }
}
