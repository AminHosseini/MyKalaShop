using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Slide;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _slideQuery.GetSlides();
            return View(model);
        }
    }
}
