using _0_Framework.Infrastructure;

namespace CommentManagement.Infrastructure.Configuration.Permissions
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "کامنت ها", new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermissions.AccessToCommentManagement, "دسترسی به منو کامنت ها"),
                        new PermissionDto(CommentPermissions.ListComments, "لیست کردن کامنت ها"),
                        new PermissionDto(CommentPermissions.SearchComments, "جستوجو کردن در کامنت ها"),
                        new PermissionDto(CommentPermissions.ConfirmComment, "تایید کردن کامنت"),
                        new PermissionDto(CommentPermissions.CancelComment, "کنسل کردن کامنت"),
                    }
                },
            };
        }
    }
}
