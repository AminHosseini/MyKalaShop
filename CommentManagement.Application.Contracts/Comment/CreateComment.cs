using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contracts.Comment
{
    public class CreateComment
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Message { get; set; }

        public long OwnerRecordId { get; set; }
        public int CommentType { get; set; }
        public string? Website { get; set; }
        public long ParentId { get; set; }

        public string ProductSlug { get; set; }
    }
}
