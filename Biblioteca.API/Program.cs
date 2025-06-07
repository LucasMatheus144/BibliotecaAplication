using System.Configuration;
using ConsoleApp.DOMAIN.Repository;
using ConsoleApp.DOMAIN.Services;
using ConsoleApp.INFRA.Implementation;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Cfg;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


//builder.Services.AddTransient<ValidatorService>();
//builder.Services.AddTransient<ClienteService>();
//builder.Services.AddTransient<LivroService>();
//builder.Services.AddTransient<RegistroEmprestimoService>();
var tipo = typeof(ClienteService).Assembly;

var serviceTipo = tipo.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Service"));

foreach( var nome in serviceTipo)
{
    builder.Services.AddTransient(nome);

}


// banco de dados
var caminhoAbsoluto = Path.GetFullPath(Path.Combine("..", "ConsoleApp.INFRA", "Config", "appsettings.json"));

var cfg = new ConfigurationBuilder()
    .AddJsonFile(caminhoAbsoluto, optional: false, reloadOnChange: true)
    .Build();

var stringConnection = cfg.GetSection("DefaultConnection")["ConnectionString"];

builder.Services.AddSingleton(c =>
{
    var config = new NHibernate.Cfg.Configuration().Configure();
    config.DataBaseIntegration(x => x.ConnectionString = stringConnection);
    return config.BuildSessionFactory();
});

builder.Services.AddTransient<IRepository, RepositoryContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
