using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;
public record SelecionarContatosQuery(int? Quantidade) : IRequest<Result<SelecionarContatosResult>>;


// Retorna uma lista de objetos selecionados. ajuda a isolar a estrutura de dados retornada.
public record SelecionarContatosResult(ImmutableList<SelecionarContatosDto> Contato);

public record SelecionarContatosDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);
