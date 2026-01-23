using Groupchat_Api.Core.Interfaces;
using Groupchat_Api.Core.Security;
using Groupchat_Api.Core.Services;
using Groupchat_Api.Data.Interfaces;
using Groupchat_Api.Data.Repos;

namespace Groupchat_Api.Extensions
{
    public static class AddScopedCollection
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<IGroupService, GroupService>();

            return services;
        }
    }
}