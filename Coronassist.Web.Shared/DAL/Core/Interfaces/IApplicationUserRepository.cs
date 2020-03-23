using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<(IdentityResult identityResult, string role, string userId, UserToken Token)> CreateAsync(Usermodel user);
        Task<(ApplicationUser ApplicationUser, string Role, bool IsSuccess, UserToken Token)> AuthenticateAsync(UserDto userDto);
        Task<ICollection<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(string username);
        Task<List<ApplicationUser>> GetUsersByRoleAsync(string role);
        Task<int> GetUsersCount();
        Task<bool> DeleteUser(string Id);
    }
}
