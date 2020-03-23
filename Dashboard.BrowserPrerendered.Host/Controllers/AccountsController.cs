using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skclusive.Blazor.Dashboard.BrowserPrerendered.Host.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IApplicationUserRepository ApplicationUserRepository;
        public AccountsController(IApplicationUserRepository applicationUserRepository)
        {
            ApplicationUserRepository = applicationUserRepository;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserDto userDto)
        {
            try
            {
                var login = await ApplicationUserRepository.AuthenticateAsync(userDto);
                if (login.ApplicationUser != null)
                    return Ok(new LoginView { IsSuccess = login.IsSuccess, User = login.ApplicationUser, Role = login.Role, Token = login.Token.Token });
                else
                    return Ok(null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usermodel username)
        {
            try
            {
                var login = await ApplicationUserRepository.CreateAsync(username);
                return Ok(new LoginView { IsSuccess = login.identityResult.Succeeded, User = new ApplicationUser { Id = login.userId }, Role = login.role, Token = login.Token.Token , Errors = login.identityResult.Errors.Select(error => error.Description).ToList()});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}