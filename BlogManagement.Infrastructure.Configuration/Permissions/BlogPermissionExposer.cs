using _0_Framework.Infrastructure;
using BlogManagement.Infrastructure.Configuration.Permissions;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "گروه_مقالات", new List<PermissionDto>
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
            };
        }
    }
}
