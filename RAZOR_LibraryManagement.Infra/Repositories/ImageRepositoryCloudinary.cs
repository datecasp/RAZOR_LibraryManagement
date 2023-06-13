using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RAZOR_LibraryManagement.Domain.Interfaces;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class ImageRepositoryCloudinary : IImageRepository
    {
        private readonly Account _account;
        public ImageRepositoryCloudinary(IConfiguration configuration)
        {
            _account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadImageASync(IFormFile file)
        {
            var client = new Cloudinary(_account);
            var uploadFileResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName
                });

            if(uploadFileResult!= null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadFileResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
