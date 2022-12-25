using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Product;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productQuery.GetLatestArrivals();
            return View(model);
        }
    }
}
