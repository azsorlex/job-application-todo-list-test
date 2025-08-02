using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

await app.RunAsync();

static void ConfigureServices(this IServiceCollection services)
{
    services.AddOpenApi();
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp", policy =>
          policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
    });

    services
        .AddControllers()
        .AddApplicationPart(Assembly.Load("TodoListApi.Presentation"));
}