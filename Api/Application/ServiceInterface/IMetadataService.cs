using Core.Domain.DTOS;
using Core.Domain.Models;
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
        Task EditAsync(MetadataEditDTO obj);
        Task PostAsync(MetadataPostDTO obj);
    }
}
