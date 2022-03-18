using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.Business.Extensions;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using Rookie.Ecom.MetaShop.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class MetaIdentityUserService : IMetaIdentityUserService
    {
        private readonly UserManager<MetaIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public MetaIdentityUserService(UserManager<MetaIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<MetaIdentityUserDto> GetById(string id)
        {
            MetaIdentityUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            return _mapper.Map<MetaIdentityUserDto>(user);
        }

        public async Task<PagedResponseModel<MetaIdentityUserDto>> PagedQueryAsync(string username, int page, int limit)
        {
            var query = _userManager.Users;
            query = query.Where(x => string.IsNullOrEmpty(username) || x.UserName.Contains(username));

            query = query.OrderBy(x => x.UserName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            IEnumerable<MetaIdentityUserDto> users = _mapper.Map<IEnumerable<MetaIdentityUserDto>>(assets.Items);

            List<MetaIdentityUserDto> listUsers = users.ToList();
            for (int i = 0; i < listUsers.Count; i++)
            {
                var tmp = _mapper.Map<MetaIdentityUser>(listUsers[i]);
                var roles = await _userManager.GetRolesAsync(tmp);
                listUsers[i].Roles = roles.ToList();
            }
            return new PagedResponseModel<MetaIdentityUserDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = listUsers.AsEnumerable()
            };
        }

        public async Task<IdentityResult> Register(UserRegistrationDto request, string role)
        {
            var userCheck = await _userManager.FindByEmailAsync(request.Email);
            var roleCheck = await _roleManager.FindByNameAsync(role);
            if (userCheck != null)
            {
                return IdentityResult.Failed(
                    new IdentityError[] {
                      new IdentityError{
                         Code = "0001",
                         Description = "This email used"
                      }
                    }
                    );
            }
            else if (roleCheck == null)
            {
                return IdentityResult.Failed(
                   new IdentityError[] {
                      new IdentityError{
                         Code = "0002",
                         Description = "This role doesn't exist"
                      }
                   }
                   );
            }
            else
            {
                /*var user = new MetaIdentityUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Email,
                    NormalizedUserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Line1 = request.Line1,
                    Line2 = request.Line2,
                    Province = request.Province,
                    Country = request.Country
                };*/

                var user = _mapper.Map<MetaIdentityUser>(request);
                user.PhoneNumberConfirmed = user.EmailConfirmed = true;
                user.UserName = user.NormalizedUserName = user.Email;

                var result = await _userManager.CreateAsync(user, request.Password);


                result = _userManager.AddToRoleAsync(user, role).Result;

                result =
                _userManager.AddClaimsAsync(
                    user,
                    new Claim[]
                    {
                            new Claim(JwtClaimTypes.Email, user.Email),
                            new Claim(JwtClaimTypes.Name, user.FirstName),
                            new Claim(JwtClaimTypes.GivenName, user.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, user.LastName),
                            new Claim(JwtClaimTypes.Role, role)
                    }
                ).Result;
                return result;
            }

        }
    }
}
