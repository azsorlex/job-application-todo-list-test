using System.Reflection;
using TodoListApi.Application.Services;
using TodoListApi.Application.Services.IServices;
using TodoListApi.Infrastructure.Repositories;
using TodoListApi.Infrastructure.Repositories.IRepositories;

const string ANGULAR_CORS_POLICY_NAME = "AllowAngularApp";

var builder = WebApplication.CreateBuilder(args);
ConfigureServices();

var app = builder.Build();
Configure();

await app.RunAsync();

void ConfigureServices()
{
    builder.Services.AddOpenApi();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(ANGULAR_CORS_POLICY_NAME, policy =>
          policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
    });

    // Register Controllers
    builder.Services
        .AddControllers()
        .AddApplicationPart(Assembly.Load("TodoListApi.Presentation"));

    // Register Services
    builder.Services.AddScoped<ITodosService, TodosService>();

    // Register Repositories
    builder.Services.AddSingleton<ITodosRepository, TodosRepository>(); // Using Singleton so the in-memory store isn't cleared.
}

void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UsePathBase("/api");
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors(ANGULAR_CORS_POLICY_NAME);
}