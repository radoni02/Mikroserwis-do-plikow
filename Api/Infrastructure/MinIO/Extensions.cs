using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MinIO
{
    public static class Extensions
    {
        //public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        //where TOptions : new()
        //{
        //    var options = new TOptions();
        //    configuration.GetSection(sectionName).Bind(options);
        //    return options;
        //}
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
        //public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var minioParamiters = configuration.GetOptions<MinIOParameters>("Minio");
        //    //services.AddSingleton(options);
        //    return services;
        //}
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMinIOFacade, MinIOFacade>();
            var minioOptions = configuration.GetOptions<MinIOParameters>("Minio");
            services.AddSingleton(minioOptions);

            return services;
            //services.AddScoped<IMinIOFacade, MinIOFacade>();
            //services.AddMinio(configuration);
            //return services;
        }
    }
}
