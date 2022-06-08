using BusinessCaseStudyService.Repo;
using BusinessCaseStudyService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessCaseStudyService.Utilities
{
    public static class DiRegistry
    {
        public static IServiceCollection TMEServiceDescriptors(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService>();
            //services.AddTransient<IDbLogger, DbLogger>();
            return services;
        }
    }
}
