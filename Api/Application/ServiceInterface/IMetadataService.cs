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
        Task<FileEntity> GetOneFile(Guid id);
        Task<List<FileEntity>> DeleteAsync(Guid id);
        Task<FileEntity> EditAsync(FileEntity obj);
    }
}
