using Application.ServiceInterface;
using Core.Domain.Abstractions;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
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
        public async Task<FileEntity> GetOneFileAsync(Guid id)
        {
            var file = await _repo.GetOneFile(id);
            if(file==null)
            {
                throw new BadInputException(id);
            }
            return file;
        }
        public async Task DeleteAsync(Guid id)
        {
            
            var file = await _repo.GetOneFile(id);
            if (file == null)
            {
                throw new BadInputException(id);
            }
            await _repo.DeleteFileAsync(file);
        }

        public async Task EditAsync(MetadataEditDTO obj)
        {
            if (obj== null)
            {
                throw new EmptyInputException();
            }
            var file = await _repo.GetOneFile(obj.Id);
            if (file == null)
            {
                throw new EmptyInputException();
            }
            file.Id = obj.Id;
            file.Name = obj.Name;
            file.Type = obj.Type;
            file.UpdateTime = DateTime.Now;
            await _repo.EditFileAsync(file);
        }
        public async Task PostAsync(MetadataPostDTO obj)
        {
            if (obj == null)
            {
                throw new EmptyInputException();
            }
            var file = new FileEntity(obj.Name, obj.Type);
            await _repo.PostFileAsync(file);
        }
    }
}
