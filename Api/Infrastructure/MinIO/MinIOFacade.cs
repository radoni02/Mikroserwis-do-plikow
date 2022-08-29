using Application.FileModels;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.MinIO
{
    public class MinIOFacade : IMinIOFacade
    {
        private readonly MinioClient _minio;
        private readonly FileRules  _fileRules;
        public MinIOFacade(MinIOParameters parameters,FileRules fileRules)
        {
            _minio = new MinioClient()
                   .WithEndpoint(parameters.Endpoint,parameters.Port)
                   .WithCredentials(parameters.AccessKey,parameters.SecretKey)
                   .Build();
            _fileRules = fileRules;
        }

        public async Task UpoladFileAsync(IFormFile file, Guid fileId)
        {
          
            await using var stream = file.OpenReadStream();
            var bucketName = CreateBucketName(file.FileName);
            await BucktExist(bucketName);

            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileId.ToString())
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(file.ContentType);
            try
            {
                await _minio.PutObjectAsync(args);
            }
            catch
            {
                throw new UnableToUplaodFileException(fileId);
            }


        }
        public async Task DownloadFileAsync(FileQuery filePayload)
        {
            var bucketName = CreateBucketName(filePayload.ContentType);
            await BucktExist(bucketName);
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePayload.MinioID.ToString())
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(filePayload.Stream);
                    filePayload.Stream.Seek(0, SeekOrigin.Begin);
                });
            try
            {
                await _minio.GetObjectAsync(args);
            }
            catch
            {
                throw new UnableToDownladException(filePayload.MinioID);
            }


        }

        public async Task DeleteFileAsync(string contentType,Guid minioId)
        {
            var bucketName = CreateBucketName(contentType);

            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(minioId.ToString());
            try
            {
                await _minio.RemoveObjectAsync(args);
            }
            catch
            {
                throw new UnableToDeleteException(minioId);
            }
        }
        public async Task BucktExist(string bucketName)
        {
            var exist = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if(!exist)
            {
                await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }
        }
        private string CreateBucketName(string OldName)
        {
            var oName = _fileRules.NameTolower(OldName);
            oName = _fileRules.ChangeFileName(OldName);
            if (_fileRules.Isvalid(oName) == false)
            {
                throw new InvalidTypeException(oName);
            }
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            string newName = rgx.Replace(OldName, "-").ToLower();
            return newName;
        }
    }
}
