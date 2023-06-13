using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsyncService(IFormFile file);
    }
}
