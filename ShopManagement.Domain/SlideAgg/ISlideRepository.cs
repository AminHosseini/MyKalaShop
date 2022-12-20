using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Slide;

namespace InventoryManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        List<SlideViewModel> GetSlides();
        EditSlide GetDetails(long id);
    }
}
