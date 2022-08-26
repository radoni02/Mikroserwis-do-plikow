using Application.FileModels;
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
        //private readonly MinIOParameters _parameters;
        public MinIOFacade(MinIOParameters parameters)
        {
            //_parameters = parameters;
            _minio = new MinioClient()
                   .WithEndpoint(parameters.Endpoint,parameters.Port)
                   .WithCredentials(parameters.AccessKey,parameters.SecretKey)
                   //.WithSSL()
                   .Build();
        }

        public async Task UpoladFileAsync(IFormFile file, Guid fileId)
        {
            await using var stream = file.OpenReadStream();
            var bucketName = CreateBucketName(file.ContentType);
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
                throw new Exception();
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
                throw new Exception();
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
                throw new Exception();
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
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            string newName = rgx.Replace(OldName, "-").ToLower();
            return newName;
        }
    }
}
