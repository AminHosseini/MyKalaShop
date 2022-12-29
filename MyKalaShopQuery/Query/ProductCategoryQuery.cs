using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Product;
using MyKalaShopQuery.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Data;

namespace MyKalaShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context,
            InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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

        public ProductCategoryQueryView GetProductCategoriesWithProductsBySlug(string slug)
        {
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .AsNoTracking().ToList();

            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.IsAvailable })
                .AsNoTracking().ToList();

            var productCategory = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryView()
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in productCategory.Products)
            {
                var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                var price = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                if (customerDiscount != null)
                {
                    product.DiscountRate = customerDiscount.DiscountRate;
                    product.DiscountEndDate = customerDiscount.EndDate.ToDiscountFormat();
                    product.HasDiscount = customerDiscount.DiscountRate > 0;

                    if (price != null)
                    {
                        product.UnitPrice = price.UnitPrice;
                        product.IsAvailable = price.IsAvailable;

                        var discountAmount = Math.Round(product.UnitPrice * product.DiscountRate) / 100;
                        product.PriceWithDiscount = product.UnitPrice - discountAmount;
                    }
                }
            }

            productCategory.Products = productCategory.Products.Where(x => x.IsAvailable).ToList();

            return productCategory;
        }

        private static List<ProductQueryView> MapProducts(List<Product> products)
        {
            return products.Select(x => new ProductQueryView()
            {
                Id = x.Id,
                Name = x.Name,
                PicturePath = x.PicturePath,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}
