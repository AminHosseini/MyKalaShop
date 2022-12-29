using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore.Data;

namespace MyKalaShopQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryView> GetSlides()
        {
            return _context.Slides
                .Where(x => !x.IsDeleted)
                .Select(x => new SlideQueryView()
                {
                    Title = x.Title,
                    Text = x.Text,
                    Heading = x.Heading,
                    ButtonText = x.ButtonText,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PicturePath = x.PicturePath,
                    Link = x.Link
                }).AsNoTracking().ToList();
        }
    }
}
