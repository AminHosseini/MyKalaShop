using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide model);
        OperationResult Edit(EditSlide model);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        List<SlideViewModel> GetSlides();
        EditSlide GetDetails(long id);
    }
}
