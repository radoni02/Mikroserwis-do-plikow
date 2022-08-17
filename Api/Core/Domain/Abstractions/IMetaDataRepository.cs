using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Abstractions
{
    public interface IMetaDataRepository
    {
        Task<List<FileEntity>> GetFilesAsync();
        Task<FileEntity> GetOneFile(Guid id);
        Task DeleteFileAsync(FileEntity obj);
        Task EditFileAsync(FileEntity obj);
        Task PostFileAsync(FileEntity obj);
    }
}
