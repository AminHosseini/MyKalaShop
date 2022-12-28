using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Application.Contracts.Product;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class InventoryController : Controller
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public InventoryController(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.ListInventory)]
        public IActionResult Index(SearchInventory model)
        {
            ViewBag.Products = GetProducts();
            var inventoryList = _inventoryApplication.Search(model);
            return View(inventoryList);
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult Create()
        {
            ViewBag.Products = GetProducts();
            return PartialView("_Create", new CreateInventory());
        }

        [HttpPost]
        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult Create(CreateInventory model)
        {
            var operationResult = _inventoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = GetProducts();
            var model = _inventoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult Edit(EditInventory model)
        {
            var operationResult = _inventoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.IncreaseInventory)]
        public IActionResult Increase(long id)
        {
            return PartialView("_Increase", new IncreaseInventory() { Id = id });
        }

        [HttpPost]
        [NeedsPermission(InventoryPermissions.IncreaseInventory)]
        public IActionResult Increase(IncreaseInventory model)
        {
            var operationResult = _inventoryApplication.Increase(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.DecreaseInventory)]
        public IActionResult Decrease(long id)
        {
            return PartialView("_Decrease", new DecreaseInventory() { Id = id });
        }

        [HttpPost]
        [NeedsPermission(InventoryPermissions.DecreaseInventory)]
        public IActionResult Decrease(DecreaseInventory model)
        {
            var operationResult = _inventoryApplication.Decrease(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(InventoryPermissions.GetInventoryOperationsLog)]
        public IActionResult OperationsLog(long id)
        {
            var log = _inventoryApplication.GetLog(id);
            return PartialView("_OperationLog", log);
        }

        private SelectList GetProducts()
        {
            return new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
    }
}
