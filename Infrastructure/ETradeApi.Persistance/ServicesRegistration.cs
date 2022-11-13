using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Repositories.CustomerRepository;
using ETradeApi.Application.Repositories.ProductRepository;
using ETradeApi.Domain.Identity;
using ETradeApi.Persistence.Context;
using ETradeApi.Persistence.Repositories.CustomerRepository;
using ETradeApi.Persistence.Repositories.ProductRepository;
using ETradeApi.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeApi.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETradeDbContext>(options => options.UseSqlServer("Data Source=SEFAONUR\\SQLEXPRESS;Initial Catalog=ETradeApi;User ID=sa;Password=3394320,TrustServerCertificate=True"),ServiceLifetime.Transient);
            services.AddIdentity<AppUser,AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ETradeDbContext>();
            services.AddScoped<IWriteCustomer,WriteCustomer>();
            services.AddScoped<IReadCustomer, ReadCustomer>();
            services.AddScoped<IWriteProduct, WriteProduct>();
            services.AddScoped<IReadProduct, ReadProduct>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
