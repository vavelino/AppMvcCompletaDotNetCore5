using DevIO.App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.App.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(
            this IServiceCollection services )
        {
            var conection = "Server=localhost;User Id=root;Password=;Database=WebAppMvcCompleta";

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(conection, ServerVersion.AutoDetect(conection)));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
