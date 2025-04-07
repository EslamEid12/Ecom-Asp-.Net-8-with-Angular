using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Inferastructure.Data;
using Ecom.Inferastructure.Repository;
using Ecom.Inferastructure.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Inferastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastructureConfigeration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });
            return services;
        }
    }
}
