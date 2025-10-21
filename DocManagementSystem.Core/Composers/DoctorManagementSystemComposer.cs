using DocManagementSystem.Core.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DocManagementSystem.Core.Composers
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IApiRepository>()
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }

        // example for more

        //public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        //{
        //    services.Scan(scan => scan
        //        .FromAssemblyOf<IDocumentRepository>() 

        //        .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
        //            .AsImplementedInterfaces()
        //            .WithScopedLifetime()

        //        .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
        //            .AsImplementedInterfaces()
        //            .WithScopedLifetime()

        //        .AddClasses(c => c.Where(t => t.Name.EndsWith("Manager")))
        //            .AsImplementedInterfaces()
        //            .WithScopedLifetime()
        //    );

        //    return services;
        //}
    }
}
