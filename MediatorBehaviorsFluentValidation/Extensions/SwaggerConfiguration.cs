using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MediatorBehaviorsFluentValidation.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Mediator Behaviors Fluent Validation API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.SchemaFilter<EnumSchemaFilter>();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
