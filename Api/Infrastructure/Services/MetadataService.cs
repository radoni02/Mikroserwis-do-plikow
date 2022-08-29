using Application.FileModels;
using Application.ServiceInterface;
using Core.Domain.Abstractions;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.MinIO;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMinIOFacade _minio;

        public MetadataService(IMetaDataRepository repo, IMinIOFacade minio)
        {
            _repo = repo;
            _minio = minio;
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
            await _minio.DeleteFileAsync(file.Type, file.Id);
            await _repo.DeleteFileAsync(file);
        }

        public async Task EditAsync(IFormFile fromFile, Guid id)
        {
            var file = await _repo.GetOneFile(id);
            if (file == null)
            {
                throw new EmptyInputException();
            }
            file.Id = id;
            file.Name = fromFile.FileName;
            file.Type = fromFile.ContentType;
            file.UpdateTime = DateTime.Now;
            await _minio.DeleteFileAsync(file.Type, file.Id);
            await _minio.UpoladFileAsync(fromFile, file.Id);
            await _repo.EditFileAsync(file);
        }

        public async Task PostAsync(IFormFile fromFile)
        {
            var file = new FileEntity(fromFile.FileName, fromFile.ContentType);
            if (file == null)
            {
                throw new EmptyInputException();
            }
            await _minio.UpoladFileAsync(fromFile, file.Id);
            await _repo.PostFileAsync(file);
        }

        public async Task<FileQuery> DownloadFileAsync(Guid id)
        {
            var file = await _repo.GetOneFile(id);
            if (file == null)
            {
                throw new BadInputException(id);
            }
            var fileQuery = new FileQuery(new MemoryStream(), file.Type, file.Name);
            fileQuery.MinioID = file.Id;
            await _minio.DownloadFileAsync(fileQuery);
            return fileQuery;
        }
    }
}
