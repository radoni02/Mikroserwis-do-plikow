using Application.FileModels;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterface
{
    public interface IMetadataService
    {
        Task<List<FileEntity>> GetFilesAsync();
        Task<FileEntity> GetOneFileAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task EditAsync(IFormFile fromFile,Guid id);
        Task PostAsync(IFormFile fromFile);
        Task<FileQuery> DownloadFileAsync(Guid id);
    }
}
