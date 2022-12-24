using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Slide;
using InventoryManagement.Domain.SlideAgg;

namespace InventoryManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _repository;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide model)
        {
            var operationResult = new OperationResult();

            var pictureRoot = "UploadedPictures/Slides";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            var slide = new Slide(picturePath, model.PictureAlt, model.PictureTitle,
                model.Heading, model.Title, model.Text, model.ButtonText, model.Link);

            _repository.Create(slide);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditSlide model)
        {
            var operationResult = new OperationResult();
            var slide = _repository.Get(model.Id);

            if (slide == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.Title == model.Title && x.Id != model.Id))
                return operationResult.Failed();

            var pictureRoot = "UploadedPictures/Slides";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            slide.Edit(picturePath, model.PictureAlt, model.PictureTitle,
                model.Heading, model.Title, model.Text, model.ButtonText, model.Link);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _repository.GetSlides();
        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            slide.Delete();
            _repository.Save();

            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            slide.Restore();
            _repository.Save();

            return operationResult.Succeeded();
        }
    }
}
