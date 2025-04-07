using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Inferastructure.Repository.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string Src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", Src);
            if (Directory.Exists(ImageDirectory) is not true) 
            { 
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var Item in files) {
                var ImageName=Item.FileName;
                var ImageSrc = $"Images/{Src}/{ImageName}";
                var root=Path.Combine(ImageDirectory, ImageName);
                using (FileStream stream = new FileStream(root,FileMode.Create))
                {
                   await Item.CopyToAsync(stream);
                }
                SaveImageSrc.Add(ImageSrc); 
            }
           return SaveImageSrc;

        }

        public void DeleteImageAsync(string Src)
        {
            var info=_fileProvider.GetFileInfo(Src);
            var root=info.PhysicalPath;
            File.Delete(root);
        }
    }
}
