using ApiPersonajes.Data;
using ApiPersonajes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration
    .GetConnectionString("SqlPersonajes");

builder.Services.AddTransient<PersonajesSerieRepository>();
builder.Services.AddDbContext<PersonajesSeriesContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Personajes Examen",
        Description = "Api Crud de Personajes",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
        , name: "Api Crud Personajes Examen v1");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();