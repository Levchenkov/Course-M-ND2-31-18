using Data.Contracts.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Contracts.Interfaces
{
    public interface IAppRoleManager : IDisposable
    {
        IQueryable<Role> Roles { get; }

        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        bool Equals(object obj);
        Task<Role> FindByIdAsync(string roleId);
        Task<Role> FindByNameAsync(string roleName);
        int GetHashCode();
        Task<bool> RoleExistsAsync(string roleName);
        string ToString();
        Task<IdentityResult> UpdateAsync(Role role);
    }
}
