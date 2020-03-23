using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class UserApplicationRepository : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>, IApplicationUserRepository
    {
        private readonly IConfiguration configuration;
        public UserApplicationRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, IConfiguration configuration) : base(userManager, roleManager, options)
        {
            this.configuration = configuration;
        }

        public async Task<(ApplicationUser ApplicationUser, string Role, bool IsSuccess, UserToken Token)> AuthenticateAsync(UserDto userDto)
        {
            var account = await UserManager.FindByNameAsync(userDto.Email);
            if (account != null)
            {
                var available = await UserManager.CheckPasswordAsync(account, userDto.Password);
                if (available)
                {
                    var role = await GetRole(account.Id);
                    var token = BuildToken(account.Email);
                    return (account, role, true, token);
                }
                else
                {
                    return (null, "", false, null);
                }



            }
            else
                return (null, "", false, null);
        }

        private async Task<string> GetRole(string Id)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(Id);
                if (user != null)
                {
                    var userRole = await UserManager.GetRolesAsync(user);
                    return userRole[0];
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(IdentityResult identityResult, string role, string userId, UserToken Token)> CreateAsync(Usermodel user)
        {
                try
                {
                    var _user = new ApplicationUser
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = user.Email,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        Province = user.Province,
                        FullAddress = user.FullAddress,
                        PhoneNumber = user.PhoneNumber
                    };

                    //check roles
                    if (!await RoleManager.RoleExistsAsync(user.Role))
                    {
                        var identityRole = new IdentityRole();
                        identityRole.Name = user.Role;
                        var results = await RoleManager.CreateAsync(identityRole);
                        var token = BuildToken(_user.Email);
                        return (results, user.Role, _user.Id, token);
                    }
                    var result = await UserManager.CreateAsync(_user, user.Password);
                    if (result.Succeeded)
                    {
                        var addRole = await UserManager.AddToRoleAsync(_user, user.Role);
                        var _token = BuildToken(_user.Email);
                        return (result, user.Role, _user.Id, _token);
                    }
                    else
                    {
                        return (result, user.Role, _user.Id, null);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        public async Task<ICollection<ApplicationUser>> GetAll()
        {
            return await UserManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetById(string username)
        {
            var account = await UserManager.FindByNameAsync(username);
            return account;
        }

        public async Task<List<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            try
            {
                var members = await UserManager.GetUsersInRoleAsync(role);
                return members.Select(member => new ApplicationUser
                {
                    Id = member.Id,
                    Fullname = member.Fullname,
                    PhoneNumber = member.PhoneNumber,
                    Email = member.Email,
                    FullAddress = member.FullAddress
                   
                    //Houses = member.Houses.Select(house => new House
                    //{
                    //    Id = house.Id
                    //}).ToList()
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private UserToken BuildToken(string Email)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Email),
                new Claim(ClaimTypes.Name, Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(365);
            JwtSecurityToken token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        public async Task<int> GetUsersCount()
        {
            return  UserManager.Users.Count();
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            if (user != null)
            {
                var _user = await UserManager.DeleteAsync(user);
                if (_user.Succeeded)
                {
                    return _user.Succeeded;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}
