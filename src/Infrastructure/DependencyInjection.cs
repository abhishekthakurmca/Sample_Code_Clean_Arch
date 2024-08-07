using Anubis.Application.Common.Interfaces;
using Anubis.Infrastructure.Files;
using Anubis.Infrastructure.Persistence;
using Anubis.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anubis.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("AnubisDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService,Infrastructure.Identity.IdentityService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IExcelConverter, ExcelConverter>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IGoogleService, GoogleService>();
            services.AddTransient<IDapper, Anubis.Infrastructure.Services.Dapper>();

            return services;
        }
    }
}
