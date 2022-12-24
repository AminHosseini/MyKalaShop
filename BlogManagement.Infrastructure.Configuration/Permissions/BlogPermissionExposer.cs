using _0_Framework.Infrastructure;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "مدیریت سیستم بلاگینگ", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.AccessToBlogManagement, "دسترسی به منو سیستم بلاگینگ")
                    }
                },
                {
                    "گروه مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.AccessToArticleCategories, "دسترسی به منو گروه مقالات"),
                        new PermissionDto(BlogPermissions.ListArticleCategories, "لیست کردن گروه مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticleCategories, "جستوجو در گروه مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticleCategory, "ایجاد گروه مقاله"),
                        new PermissionDto(BlogPermissions.EditArticleCategory, "ویرایش گروه مقاله")
                    }
                },
                {
                    "مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.AccessToArticles, "دسترسی به منو مقالات"),
                        new PermissionDto(BlogPermissions.ListArticles, "لیست کردن مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticles, "جستوجو در مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticle, "ایجاد مقاله"),
                        new PermissionDto(BlogPermissions.EditArticle, "ویرایش مقاله")
                    }
                },
            };
        }
    }
}
