using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Web.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Contact APIs",
                Version = "v1",
            });
            c.OperationFilter<SwaggerFileOperationFilter>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

        });


        return services;
    }
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration Configuration)
    {
        var swaggerOptions = new Options.SwaggerOptions();
        Configuration.GetSection(nameof(Options.SwaggerOptions)).Bind(swaggerOptions);

        app.UseSwagger(option =>
        {
            option.RouteTemplate = swaggerOptions.jsonRoute;
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(swaggerOptions.UIEndpint, swaggerOptions.Description);
            c.DocumentTitle = "Adel Contact Test Title Documentation";
            //c.DocExpansion(DocExpansion.None);
            c.DocExpansion(DocExpansion.List);

            c.RoutePrefix = "swagger-ui";
        });
        return app;
    }

}
