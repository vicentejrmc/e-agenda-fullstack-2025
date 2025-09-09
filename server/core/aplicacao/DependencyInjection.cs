using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace eAgenda.Core.Aplicacao;

public static class DependencyInjection
{
    // Configuração da camada de aplicação, incluindo MediatR e Serilog.
    public static IServiceCollection AddCamadaAplicacao(
        this IServiceCollection services,
        ILoggingBuilder logging,
        IConfiguration configuration
    )
    {
        services.AddSerilogConfig(logging, configuration); // Configuração do Serilog

        var assembly = typeof(DependencyInjection).Assembly;
        var licenseKey = configuration["AUTOMAPPER_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
            throw new Exception("A variável AUTOMAPPER_LICENSE_KEY não foi fornecida.");

        // Configuração do MediatR para injeção de dependências.
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);

            config.LicenseKey = licenseKey;
        });
        // Configuração do AutoMapper para injeção de dependencias, passando o Assembly(dll) da proria classe
        services.AddAutoMapper(config =>
        {
            config.LicenseKey = licenseKey;

        }, assembly);

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

    // Configuração do Serilog para logging com New Relic e arquivo local de logs de erros.
    private static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        var licenseKey = configuration["NEWRELIC_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
            throw new Exception("A variável NEWRELIC_LICENSE_KEY não foi fornecida.");

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // Diretório local do usuário

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "eAgenda", "erro.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error) // Logs de erro para arquivo local
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1", // Endpoint para logs
                applicationName: "e-agenda-app",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders(); // Remove outros providers de logging

        services.AddSerilog(); // Adiciona o Serilog como provider de logging
    }
}