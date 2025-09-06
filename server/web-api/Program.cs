using eAgenda.Core.Aplicacao;
using eAgenda.Infraestrutura.Orm;
using eAgenda.WebApi.Orm;

namespace eAgenda.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Injeção De Dependencias que serão usadas pelo Controller para acessar os dados distribuidos
        builder.Services
            .AddCamadaAplicacao(builder.Logging, builder.Configuration)
            .AddCamadaInfraestruturaOrm(builder.Configuration);

        builder.Services.AddControllers();

        // Swagger/OpenAPI https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //para fazer as migrações é necessario adicionar o pacote Microsoft.EntityFrameworkCore.Tools
            app.ApplyMigrations();

            app.UseSwagger(); 
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
