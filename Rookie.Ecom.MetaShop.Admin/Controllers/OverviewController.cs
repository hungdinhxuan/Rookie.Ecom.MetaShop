using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Overview;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route(Endpoints.Overview)]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;
        private readonly IOrderItemService _orderItemService;

        public OverviewController(IMetaIdentityUserService metaIdentityUserService, IOrderItemService orderItemService)
        {
            _metaIdentityUserService = metaIdentityUserService;
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public async Task<OverviewDto> GetAsync()
        {

            return new OverviewDto
            {
                TotalOrderedItem = await _orderItemService.CountAsync(),
                TotalUser = await _metaIdentityUserService.CountAsync()
            };
        }
    }
}
