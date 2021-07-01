using Domain.Interfaces.Repositories;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services)
        {
            services.AddSingleton<IAutorRepository, AutorSqlServerRepository>();
            services.AddSingleton<ILivroRepository, LivroSqlServerRepository>();
        }
    }
}
