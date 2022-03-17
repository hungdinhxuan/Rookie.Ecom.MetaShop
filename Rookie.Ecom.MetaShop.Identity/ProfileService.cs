using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Rookie.Ecom.MetaShop.Identity.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<MetaIdentityUser> _userClaimsPrincipalFactory;
        private readonly UserManager<MetaIdentityUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public ProfileService(
            UserManager<MetaIdentityUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IUserClaimsPrincipalFactory<MetaIdentityUser> userClaimsPrincipalFactory)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            MetaIdentityUser user = await _userMgr.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if (_userMgr.SupportsUserRole)
            {
                IList<string> roles = await _userMgr.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleMgr.SupportsRoleClaims)
                    {
                        IdentityRole role = await _roleMgr.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleMgr.GetClaimsAsync(role));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            MetaIdentityUser user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
    /* public class ProfileService : IProfileService
     {
         public ProfileService() { }
         public Task GetProfileDataAsync(ProfileDataRequestContext context)
         {
             var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
             context.IssuedClaims.AddRange(roleClaims);
             return Task.CompletedTask;
         }

         public Task IsActiveAsync(IsActiveContext context)
         {
             return Task.CompletedTask;
         }
     }*/
}
