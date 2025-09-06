using eAgenda.Core.Dominio.ModuloCategoria;
using eAgenda.Core.Dominio.ModuloCompromisso;
using eAgenda.Core.Dominio.ModuloContato;
using eAgenda.Core.Dominio.ModuloDespesa;
using eAgenda.Core.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.Infraestrutura.Orm.MapeadorDespesa;
using eAgenda.Infraestrutura.Orm.ModuloCategoria;
using eAgenda.Infraestrutura.Orm.ModuloCompromisso;
using eAgenda.Infraestrutura.Orm.ModuloContato;
using eAgenda.Infraestrutura.Orm.ModuloTarefa;
using eAgenda.Core.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eAgenda.Infraestrutura.Orm;

public static class DependencyInjection
{
    // Configuração da camada de infraestrutura usando ORM (Entity Framework Core).
    public static IServiceCollection AddCamadaInfraestruturaOrm(this IServiceCollection services, IConfiguration configuration)
    {
        // Registro dos repositórios para injeção de dependências
        services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmOrm>();
        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmOrm>();
        services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmOrm>();

        // Configuração do Entity Framework Core com PostgreSQL
        services.AddEntityFrameworkConfig(configuration);

        return services;
    }

    // Configuração do Entity Framework Core com PostgreSQL
    private static void AddEntityFrameworkConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration["SQL_CONNECTION_STRING"]; // Obtém a string de conexão do arquivo de configuração

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("A variável SQL_CONNECTION_STRING não foi fornecida."); // Verifica se a string de conexão foi fornecida

        // Configuração do DbContext com retry em falhas de conexão com o banco de dados PostgreSQL
        services.AddDbContext<IUnitOfWork, AppDbContext>(options =>
            options.UseNpgsql(connectionString, (opt) => opt.EnableRetryOnFailure(3)));
    }
}
