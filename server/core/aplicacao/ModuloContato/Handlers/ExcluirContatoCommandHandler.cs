using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;
public class ExcluirContatoCommandHandler(
    IRepositorioContato repositorioContato,
    IUnitOfWork unitOfWork,
    ILogger<ExcluirContatoCommandHandler> logger
) : IRequestHandler<ExcluirContatoCommand, Result<ExcluirContatoResult>>
{
    public async Task<Result<ExcluirContatoResult>> Handle(ExcluirContatoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await repositorioContato.ExcluirAsync(command.Id);
            await unitOfWork.CommitAsync();

            var result = new ExcluirContatoResult();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao excluir contato com Id {Id}", command);

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
