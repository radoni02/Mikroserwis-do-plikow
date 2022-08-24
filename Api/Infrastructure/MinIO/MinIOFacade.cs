using Microsoft.AspNetCore.Http;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MinIO
{
    public class MinIOFacade 
    {
        private readonly MinioClient _minio;

        public MinIOFacade(MinIOParameters parameters)
        {
            _minio = new MinioClient()
                   .WithEndpoint(parameters.Endpoint)
                   .WithCredentials(parameters.AccessKey,parameters.SecretKey)
                   .WithSSL()
                   .Build();
        }

        //public async Task UpoladFileAsync(IFormFile)
        //{

        //}
    }
}
