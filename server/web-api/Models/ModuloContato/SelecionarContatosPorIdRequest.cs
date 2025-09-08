using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using System.Collections.Immutable;

namespace eAgenda.WebApi.Models.ModuloContato;

public record SelecionarContatosPorIdRequest(Guid Id);

public record SelecionarContatosPorIdResponse(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo,
    ImmutableList<DetalhesComprimissoConatatoDto> Compromissos
);