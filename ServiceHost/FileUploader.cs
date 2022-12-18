using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnviorment;

        public FileUploader(IWebHostEnvironment webHostEnviorment)
        {
            _webHostEnviorment = webHostEnviorment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            string directory = $"{_webHostEnviorment.WebRootPath}//UploadedFiles//{path}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string fileName = $"{Guid.NewGuid().ToString()}___{file.FileName}";
            string filePath = $"{directory}//{fileName}";
            using FileStream fileStream = File.Create(filePath);
            file.CopyTo(fileStream);

            return $"{path}/{fileName}";
        }
    }
}
