using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore.Data;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture()
            {
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(SearchProductPicture model)
        {
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductName = x.Product.Name,
                    ProductId = x.ProductId,
                    PicturePath = x.PicturePath,
                    IsDeleted = x.IsDeleted
                });

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
