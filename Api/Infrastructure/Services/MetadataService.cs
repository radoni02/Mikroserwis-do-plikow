using Application.ServiceInterface;
using Core.Domain.Abstractions;
using Core.Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class MetadataService : IMetadataService
    {
        private readonly IMetaDataRepository _repo;

        public MetadataService(IMetaDataRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<FileEntity>> GetFilesAsync()
        {
            
            return await _repo.GetFilesAsync();
        }
        public async Task<FileEntity> GetOneFile(Guid id)
        {
            return await _repo.GetOneFile(id);
        }
        public async Task<List<FileEntity>> DeleteAsync(Guid id)
        {
            var file = await _repo.GetOneFile(id);
            return await _repo.DeleteFileAsync(file);
        }

        public async Task<FileEntity> EditAsync(FileEntity obj)
        {
            var file =await _repo.GetOneFile(obj.Id);
            return await _repo.EditFileAsync(file);
        }
    }
}
