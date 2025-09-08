using FluentResults;
using MediatR;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;

// record é uma classe imutável ideal para transportar dados. funciona como um DTO.
// Comando para cadastrar um novo contato.
// Retorna o resultado do cadastro, incluindo o ID do novo contato.
// DTO é um objeto que carrega dados entre processos, reduzindo chamadas. um DTO não tem lógica, apenas dados.
public record CadastrarContatoCommand(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
) : IRequest<Result<CadastrarContatoResult>>;

public record CadastrarContatoResult(Guid Id);