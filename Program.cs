using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar el DbContext con la cadena de conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controladores
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Productos API",
        Version = "v1",
        Description = "Módulo de Productos - ASP.NET Core Web API + EF Core + SQL Server"
    });
});

var app = builder.Build();

// Swagger habilitado siempre para facilitar las pruebas del examen
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Productos API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
