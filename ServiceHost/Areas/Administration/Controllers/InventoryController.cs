using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Application.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
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
        public IActionResult Index(SearchInventory model)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var inventoryList = _inventoryApplication.Search(model);
            return View(inventoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return PartialView("_Create", new CreateInventory());
        }

        [HttpPost]
        public JsonResult Create(CreateInventory model)
        {
            var operationResult = _inventoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var model = _inventoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditInventory model)
        {
            var operationResult = _inventoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Increase(long id)
        {
            return PartialView("_Increase", new IncreaseInventory() { Id = id });
        }

        [HttpPost]
        public IActionResult Increase(IncreaseInventory model)
        {
            var operationResult = _inventoryApplication.Increase(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Decrease(long id)
        {
            return PartialView("_Decrease", new DecreaseInventory() { Id = id });
        }

        [HttpPost]
        public IActionResult Decrease(DecreaseInventory model)
        {
            var operationResult = _inventoryApplication.Decrease(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult OperationsLog(long id)
        {
            var log = _inventoryApplication.GetLog(id);
            return PartialView("_OperationLog", log);
        }
    }
}
