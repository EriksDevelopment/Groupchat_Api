using Microsoft.OpenApi;

namespace Groupchat_Api.Extensions
{
    public static class AddSwaggerGenCollection
    {
        public static IServiceCollection AddSwaggerGenServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Groupchat Api",
                    Version = "v1"
                });
            });
            return services;
        }
    }
}