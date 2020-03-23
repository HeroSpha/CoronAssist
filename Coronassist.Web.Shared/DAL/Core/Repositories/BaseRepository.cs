using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class BaseRepository
    {
        public DatabaseService DatabaseService { get; set; }
        public BaseRepository(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }
    }
}
