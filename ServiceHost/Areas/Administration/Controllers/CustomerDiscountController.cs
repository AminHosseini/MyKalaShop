using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class CustomerDiscountController : Controller
    {
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public CustomerDiscountController(ICustomerDiscountApplication customerDiscountApplication, 
            IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchCustomerDiscount model)
        {
            ViewBag.Products = GetProducts();
            var customerDiscounts = _customerDiscountApplication.Search(model);
            return View(customerDiscounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = GetProducts();
            return PartialView("_Create", new CreateCustomerDiscount());
        }

        [HttpPost]
        public JsonResult Create(CreateCustomerDiscount model)
        {
            var operationResult = _customerDiscountApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = GetProducts();
            var model = _customerDiscountApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditCustomerDiscount model)
        {
            var operationResult = _customerDiscountApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        private SelectList GetProducts()
        {
            return new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
    }
}
