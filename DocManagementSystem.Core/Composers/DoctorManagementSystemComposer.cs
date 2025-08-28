using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DocManagementSystem.Core.Composers
{
    public static class ServiceComposer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var repositoryTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

            foreach (var repo in repositoryTypes)
            {
                var interfaces = repo.GetInterfaces();

                foreach (var i in interfaces)
                {
                    services.AddScoped(i, repo); 
                }
            }

            return services;
        }
    }
}
