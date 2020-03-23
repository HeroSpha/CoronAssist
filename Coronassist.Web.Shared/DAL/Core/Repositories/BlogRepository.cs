using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class BlogRepository : BaseRepository, IBlogRepository
    {
        public BlogRepository(DatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<Blog> AddBlog(Blog blog)
        {
            try
            {
                var _blog = await DatabaseService.accountContext.Blogs.AddAsync(blog);
                await DatabaseService.accountContext.SaveChangesAsync();
                return _blog.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Blog>> GetBlogs()
        {
            try
            {
                var blogs = await DatabaseService.accountContext.Blogs.ToListAsync();
                return blogs;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
