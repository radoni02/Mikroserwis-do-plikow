using Application.FileModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MinIO
{
    public interface IMinIOFacade
    {
        Task UpoladFileAsync(IFormFile file, Guid fileId);
        Task DownloadFileAsync(FileQuery filePayload);
        Task DeleteFileAsync(string contentType, Guid minioId);
    }
}
