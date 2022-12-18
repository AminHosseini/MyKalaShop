using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Framework.Application
{
    public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _allowedFormats;

        public FileExtentionLimitationAttribute(string[] allowedFormats)
        {
            _allowedFormats = allowedFormats;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var extention = Path.GetExtension(file.FileName);
            return _allowedFormats.Contains(extention);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtentionLimitation", ErrorMessage);
        }
    }
}
