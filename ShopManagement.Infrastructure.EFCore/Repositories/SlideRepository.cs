using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Slide;
using InventoryManagement.Domain.SlideAgg;
using InventoryManagement.Infrastructure.EFCore.Data;

namespace InventoryManagement.Infrastructure.EFCore.Repositories
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide()
            {
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                ButtonText = x.ButtonText,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _context.Slides.Select(x => new SlideViewModel()
            {
                Id = x.Id,
                PicturePath = x.PicturePath,
                Heading = x.Heading,
                IsDeleted = x.IsDeleted,
                Title = x.Title,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
