using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;
public record EditarContatoCommand(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
) : IRequest<Result<EditarContatoResult>>;

public record EditarContatoResult(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);
