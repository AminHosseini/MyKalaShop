using _0_Framework.Infrastructure;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class HomeController : Controller
    {
        [NeedsPermission(ShopPermissions.AccessToAdminDashboard)]
        public IActionResult Index()
        {
            return View();
        }
    }
}