using _0_Framework.Application;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BlogLand.UI.Server
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\UploadedFiles\\{path}";

            directoryPath = directoryPath.Replace("\\Server\\", "\\Client\\");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directoryPath}//{fileName}";

            using var output = File.Create(filePath);
            file.OpenReadStream().CopyTo(output);

            return $"{path}/{fileName}";
        }

        public void Delete(string path)
        {
            var files = Directory.GetFiles(_webHostEnvironment.WebRootPath + "//UploadedFiles//" + path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

        }
    }
}
