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
                    "کامنت_ها", new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermissions.AccessToCommentManagement, "دسترسی_به_سیستم_کامنت_ها"),
                        new PermissionDto(CommentPermissions.ListComments, "لیست_کامنت_ها"),
                        new PermissionDto(CommentPermissions.ConfirmComment, "تایید_کامنت"),
                        new PermissionDto(CommentPermissions.CancelComment, "کنسلـکامنت"),
                    }
                },
            };
        }
    }
}
