using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Product;

namespace MyKalaShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext context,
            InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryView> GetProductsWithProductCategory()
        {
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .AsNoTracking().ToList();

            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.IsAvailable })
                .AsNoTracking().ToList();

            var products = _context.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    PicturePath = x.PicturePath,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCategory = x.ProductCategory.Name,
                    ProductCategorySlug = x.ProductCategory.Slug
                }).AsNoTracking().ToList();

            foreach (var product in products)
            {
                var price = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var discount = customerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);

                if (price != null)
                {
                    product.UnitPrice = price.UnitPrice;
                    product.IsAvailable = price.IsAvailable;

                    if (discount != null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.DiscountEndDate = discount.EndDate.ToFarsi();
                        product.HasDiscount = discount.DiscountRate > 0;

                        var discountAmount = Math.Round(product.UnitPrice * product.DiscountRate) / 100;
                        product.PriceWithDiscount = (product.UnitPrice - discountAmount);
                    }
                }
            }

            return products.Where(x => x.IsAvailable).ToList();
        }

        public List<ProductQueryView> GetLatestArrivals()
        {
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .AsNoTracking().ToList();

            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.IsAvailable })
                .AsNoTracking().ToList();

            var products = _context.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    PicturePath = x.PicturePath,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCategory = x.ProductCategory.Name,
                    ProductCategorySlug = x.ProductCategory.Slug
                }).OrderByDescending(x => x.Id).Take(6)
                .AsNoTracking().ToList();

            foreach (var product in products)
            {
                var price = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var discount = customerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);

                if (price != null)
                {
                    product.UnitPrice = price.UnitPrice;
                    product.IsAvailable = price.IsAvailable;

                    if (discount != null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.DiscountEndDate = discount.EndDate.ToFarsi();
                        product.HasDiscount = discount.DiscountRate > 0;

                        var discountAmount = Math.Round(product.UnitPrice * product.DiscountRate) / 100;
                        product.PriceWithDiscount = (product.UnitPrice - discountAmount);
                    }
                }
            }

            return products.Where(x => x.IsAvailable).ToList();
        }
    }
}
