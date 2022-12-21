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
                    "دسترسی_به_گروه_مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.AccessToBlogManagement, "دسترسی_به_سیستم_مدیریت_گروه_مقالات")
                    }
                },
                {
                    "گروه_مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticleCategories, "لیست_گروه_مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticleCategories, "جستوجو_گروه_مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticleCategory, "ایجاد_گروه_مقاله"),
                        new PermissionDto(BlogPermissions.EditArticleCategory, "ویرایش_گروه_مقاله")
                    }
                },
                {
                    "مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticles, "لیست_مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticles, "جستوجو_مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticle, "ایجاد_مقاله"),
                        new PermissionDto(BlogPermissions.EditArticle, "ویرایش_مقاله")
                    }
                },
            };
        }
    }
}
