using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class OrderController : Controller
    {
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.ListOrders)]
        public IActionResult Index(SearchOrders model)
        {
            ViewBag.PaymentMethod = new SelectList(PaymentMethod.GetList(), "Id", "Name");
            var orders = _orderApplication.Search(model);
            return View(orders);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.GetOrderItems)]
        public IActionResult GetOrderItems(long orderId)
        {
            var orderItems = _orderApplication.GetOrderItems(orderId);
            return PartialView("_OrderItems", orderItems);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.ConfirmOrder)]
        public IActionResult Confirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.CancelOrder)]
        public IActionResult Cancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
