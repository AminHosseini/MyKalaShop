using _0_Framework.Application;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Data;
using DiscountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Comment;
using MyKalaShopQuery.Contracts.Product;
using MyKalaShopQuery.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore.Data;

namespace MyKalaShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext,
            DiscountContext discountContext, CommentContext commentContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
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

        public ProductQueryView GetProductBySlug(string slug)
        {
            var product = _context.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductPictures)
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
                    ProductCategorySlug = x.ProductCategory.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    ProductPictures = MapProductPictures(x.ProductPictures),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryView();

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .AsNoTracking().FirstOrDefault(x => x.ProductId == product.Id);

            var price = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.IsAvailable })
                .AsNoTracking().FirstOrDefault(x => x.ProductId == product.Id);

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Where(x => x.Type == CommentType.ProductComment)
                .Where(x => x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message
                }).AsNoTracking().ToList();

            if (comments != null)
                product.ProductComments = comments;

            if (price != null)
            {
                product.UnitPrice = price.UnitPrice;
                product.IsAvailable = price.IsAvailable;

                if (discount != null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.DiscountEndDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discount.DiscountRate > 0;

                    var discountAmount = Math.Round(product.UnitPrice * product.DiscountRate) / 100;
                    product.PriceWithDiscount = (product.UnitPrice - discountAmount);
                }
            }

            return product;
        }

        public List<ProductQueryView> Search(string key)
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
                    ShortDescription = x.ShortDescription,
                    PicturePath = x.PicturePath,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCategory = x.ProductCategory.Name,
                    ProductCategorySlug = x.ProductCategory.Slug
                }).AsNoTracking().ToList();

            if (!string.IsNullOrWhiteSpace(key))
                products = products.Where(x => x.Name.Contains(key) || x.ShortDescription.Contains(key)).ToList();

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
                        product.DiscountEndDate = discount.EndDate.ToDiscountFormat();
                        product.HasDiscount = discount.DiscountRate > 0;

                        var discountAmount = Math.Round(product.UnitPrice * product.DiscountRate) / 100;
                        product.PriceWithDiscount = (product.UnitPrice - discountAmount);
                    }
                }
            }

            return products.Where(x => x.IsAvailable).ToList();
        }

        private static List<ProductPictureQueryView> MapProductPictures(List<ProductPicture> productPictures)
        {
            return productPictures
                .Where(x => !x.IsDeleted)
                .Select(x => new ProductPictureQueryView()
                {
                    ProductId = x.ProductId,
                    PicturePath = x.PicturePath,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).ToList();
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory.ToList();

            foreach (var item in cartItems)
            {
                if (inventory.Any(x => x.ProductId == item.Id && x.IsAvailable))
                {
                    var inventoryProduct = inventory.FirstOrDefault(x => x.ProductId == item.Id);
                    item.IsInStock = item.Count <= inventoryProduct.CalculateCurrentCount();
                }
            }

            return cartItems;
        }
    }
}
