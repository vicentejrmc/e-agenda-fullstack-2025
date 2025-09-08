using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;
public class EditarContatoCommandHandler(
    IMapper mapper,
    IRepositorioContato repositorioContato,
    IUnitOfWork unitOfWork,
    ILogger<EditarContatoCommandHandler> logger
    ) : IRequestHandler<EditarContatoCommand, Result<EditarContatoResult>>
{
    public async Task<Result<EditarContatoResult>> Handle(EditarContatoCommand command, CancellationToken cancellationToken)
    {
        var registros = await repositorioContato.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Id.Equals(command.Id) && i.Nome.Equals(command.Nome)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um contato registrado com este nome."));

        try
        {
            var contatoEditado = mapper.Map<Contato>(command);

            await repositorioContato.EditarAsync(command.Id, contatoEditado);
            await unitOfWork.CommitAsync();

            var result = mapper.Map<EditarContatoResult>(contatoEditado);

            return Result.Ok(result);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Erro ao editar o contato com Id {Id}.", command);

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
