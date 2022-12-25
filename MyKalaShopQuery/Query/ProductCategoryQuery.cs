using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace MyKalaShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryView> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryView()
            {
                Name = x.Name,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PicturePath = x.PicturePath
            }).AsNoTracking().ToList(); ;
        }
    }
}
