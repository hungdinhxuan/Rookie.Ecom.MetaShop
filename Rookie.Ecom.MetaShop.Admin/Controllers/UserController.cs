using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route(Endpoints.User)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;

        public UserController(IMetaIdentityUserService metaIdentityUserService)
        {
            _metaIdentityUserService = metaIdentityUserService;
        }


        [HttpGet("find")]
        public async Task<PagedResponseModel<MetaIdentityUserDto>>
            FindAsync(string name, int page = 1, int limit = 10) => await _metaIdentityUserService.PagedQueryAsync(name, page, limit);


    }
}
