using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Interfaces;
using Service.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service
{

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICrudereService repo;

        public IdentityService(UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, ICrudereService _repo)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            repo = _repo;
        }

        public Task<bool> IsRoleExistsAsync(string roleName)
        {
            return roleManager.RoleExistsAsync(roleName);
        }

        public Task<IdentityRole> GetRoleAsync(string roleName)
        {
            return roleManager.FindByNameAsync(roleName);
        }

        public Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role.Name != roleName)
                return await roleManager.SetRoleNameAsync(role, roleName);
            else
                return await roleManager.UpdateAsync(role);
        }

        public async Task<IList<Claim>> GetRoleClaimsAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
                return await roleManager.GetClaimsAsync(role);
            return new List<Claim>();
        }
        public async Task<IdentityResult> CreateRoleClaimAsync(string roleName, string claimType, string claimValue)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var roleClaim = new Claim(claimType, claimValue);
            var roleClaimList = await roleManager.GetClaimsAsync(role);
            IdentityResult result = null;
            if (!roleClaimList.Any(a => a.Type == claimType && a.Value == claimValue))
            {
                try
                {
                    result = await roleManager.AddClaimAsync(role, roleClaim);
                    if (!result.Succeeded)
                        return result;
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }

            }
            var menu = repo.Where<ApplicationMenu>(m => m.Code == claimType && m.ParentId != "None");
            if (menu.Any())
            {
                try
                {
                    if (!roleClaimList.Any(c => c.Type == menu.First().ParentId && c.Value == "View"))
                    {
                        result = await roleManager.AddClaimAsync(role, new Claim(menu.First().ParentId, "View"));
                    }
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }
            }
            return result;
        }

        public async Task<IdentityResult> AddUserRoleAsync(string userId, string roleName)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (!await userManager.IsInRoleAsync(user, roleName))
                    return await userManager.AddToRoleAsync(user, roleName);
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
            return null;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var userRoles = await userManager.GetRolesAsync(user);
            return userRoles;
        }

        public async Task<IEnumerable<ApplicationMenu>> GetMenusInfo(string userId)
        {
            IList<string> roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(userId));
            List<ApplicationMenu> userMenus = new List<ApplicationMenu>();

            foreach (var roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                var roleClaims = await roleManager.GetClaimsAsync(role);
                var claimTypes = roleClaims.Where(c => c.Value == "View").Select(s => s.Type);
                var cachedMenus = repo.Where<ApplicationMenu>(m => m.IsVisible == true, false)
                        .Join(claimTypes, m => m.Code, c => c, (m, c) => new { ApplicationMenu = m, Claim = c })
                        .Select(s => s.ApplicationMenu).ToList();
                userMenus.AddRange(cachedMenus);
            }
            return userMenus.GroupBy(p => p.Id).Select(m => m.First());

        }
    }
}
