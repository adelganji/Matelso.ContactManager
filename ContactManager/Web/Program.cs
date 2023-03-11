using MediatR;
using Web.Extensions;
using Infrastructure.DataBase;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var Configuration = builder.Configuration;

// Add CORS
services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//
var assembly = AppDomain.CurrentDomain.Load("Application");
services.AddMediatR(assembly);

//Custome service and Store Injection is here
Web.Helpers.CustomServiceInjection.Config(services, Configuration);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerDocumentation();


var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Set connectionString
var connectionStrings = new Web.Options.ConnectionStrings();
Configuration.GetSection(nameof(Web.Options.ConnectionStrings)).Bind(connectionStrings);
DataBase.Config.DefaultConnectionString = connectionStrings.DefaultConnection;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(Configuration);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
