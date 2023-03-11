using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace Web.Helpers;
internal class ConfigServices
{
    //internal static void Config(IServiceCollection services, IConfiguration Configuration)
    //{

    //    //services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
    //    //{
    //    //    options.AllowSynchronousIO = true;
    //    //});

    //    // If using IIS:
    //    // services.Configure<IISServerOptions>(options =>
    //    // {
    //    //     options.AllowSynchronousIO = true;
    //    // });

    //    services.Configure<CookiePolicyOptions>(options =>
    //    {
    //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    //        options.CheckConsentNeeded = context => true;
    //        options.MinimumSameSitePolicy = SameSiteMode.None;
    //    });

    //    //note : custome service Injection is here
    //    CustomServiceInjection.Config(services, Configuration);

    //    services.AddRouting(options => options.LowercaseUrls = true);

    //    //Add Newtonsoft Json Options
    //    services.AddControllers().AddNewtonsoftJson(options =>
    //    {
    //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //    }
    //    );

    //    // Add CORS
    //    services.AddCors(options =>
    //        options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    //    services.AddSwaggerGen(s =>
    //    {
    //        s.SwaggerDoc("v1", new OpenApiInfo()
    //        {
    //            Version = "v1",
    //            Title = "Project Odyssey API - CQRS",
    //            Description = "This is the API for Project Odyssey."
    //        });
    //        s.DescribeAllParametersInCamelCase();
    //        s.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    //        {
    //            Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
    //            Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
    //            {
    //                Implicit = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
    //                {
    //                    AuthorizationUrl = new Uri(string.Format("{0}/authorize?audience={1}", "https://project-odyssey.eu.auth0.com", "project-odyssey.io"), UriKind.Absolute),
    //                    Scopes = new Dictionary<string, string>
    //                    {
    //                        ["Scope"] = "Access the API"
    //                    }
    //                }
    //            }
    //        });
    //    });

    //    var assembly = AppDomain.CurrentDomain.Load("Application");
    //    services.AddMediatR(assembly);

    //    services.AddLogging();
    //    services.AddControllers();
    //}

}
