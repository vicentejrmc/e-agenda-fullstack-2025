using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;
public record SelecionarContatosPorIdQuery(Guid Id) : IRequest<Result<SelecionarContatosPorIdResult>>;

public record SelecionarContatosPorIdResult(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo,
    ImmutableList<DetalhesComprimissoConatatoDto> Compromissos
);

public record DetalhesComprimissoConatatoDto(
    string Assunto,
    DateTime Data,
    TimeSpan HoraInicio,
    TimeSpan HoraTermino
);