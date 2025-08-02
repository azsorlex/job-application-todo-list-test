using System.Reflection;
using TodoListApi.Application.Services;
using TodoListApi.Application.Services.IServices;
using TodoListApi.Infrastructure.Repositories;
using TodoListApi.Infrastructure.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
ConfigureServices();

var app = builder.Build();

Configure();

await app.RunAsync();
void ConfigureServices()
{
    builder.Services.AddOpenApi();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp", policy =>
          policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
    });

    builder.Services
        .AddControllers()
        .AddApplicationPart(Assembly.Load("TodoListApi.Presentation"));

    // Register Services
    builder.Services.AddScoped<ITodosService, TodosService>();

    // Register Repositories
    builder.Services.AddSingleton<ITodosRepository, TodosRepository>();
}

void Configure()
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors("AllowAngularApp");
}