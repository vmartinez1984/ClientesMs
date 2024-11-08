using AutoMapper;
using ClientesMs.Ayudantes;
using ClientesMs.ReglasDeNegocio;
using ClientesMs.Repositorios;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Debugging;
using System.Reflection;
using VMtz84.Logger.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Configura Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Lee configuración desde appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Reemplaza el logger predeterminado por Serilog
builder.Host.UseSerilog();
//Muestra el error de serilog
SelfLog.Enable(Console.Error);

// Add services to the container.
builder.Services.AddScoped<RepositorioDeCliente>();
builder.Services.AddScoped<ClienteRdN>();

//Mappers
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<Mapeador>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Clientes Api",        
        Description = "Microservico para Clientes",
        Contact = new OpenApiContact
        {
            Name = "Víctor Martínez",
            Url = new Uri("mailto:ahal_tocob@hotmail.com")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("*");
    });
});

var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(
options =>
{    
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
}
);

app.UseMiddleware<RequestResponseMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Asegúrate de cerrar el logger al final del programa
Log.CloseAndFlush();