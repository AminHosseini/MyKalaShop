using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Data;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products
                .Select(x => new EditProduct()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    ProductCategoryId = x.ProductCategoryId
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> Search(SearchProduct model)
        {
            var query = _context.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    CreationDate = x.CreationDate.ToFarsi(),
                    PicturePath = x.PicturePath,
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCategory = x.ProductCategory.Name
                });

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Code))
                query = query.Where(x => x.Code.Contains(model.Code));

            if (model.ProductCategoryId != 0)
                query = query.Where(x => x.ProductCategoryId == model.ProductCategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
