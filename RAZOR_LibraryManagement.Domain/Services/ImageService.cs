using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RAZOR_LibraryManagement.Domain.Interfaces;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> UploadImageAsyncService(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadImageASync(file);
            if(imageUrl == null)
            {
                return null;
            }
            return imageUrl;
        }
    }
}
