using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skclusive.Blazor.Dashboard.Server.Host.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository BlogRepository;
        public BlogsController(IBlogRepository blogRepository)
        {
            BlogRepository = blogRepository;
        } 
        [HttpGet("getblogs")]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                return Ok(await BlogRepository.GetBlogs());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}