using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eAgenda.Core.Aplicacao.ModuloContato.Cadastrar;

// o handler processa o comando, implementando a lógica de negócio associada ao comando.
// ele interage com o repositório para persistir os dados e usa o unit of work para garantir a atomicidade da operação.
// ele também lida com erros, retornando resultados apropriados.
// o handler é registrado na injeção de dependências para que o MediatR possa resolvê-lo quando o comando for enviado.
// IRequestHandler<TRequest, TResponse> é uma interface do MediatR que define um manipulador para um tipo específico de solicitação (TRequest) e o tipo de resposta esperado (TResponse).

public class CadastrarContatoCommandHandler(
    IRepositorioContato repositorioContato,
    IUnitOfWork unitOfWork,
    ILogger<CadastrarContatoCommandHandler> logger
) : IRequestHandler<CadastrarContatoCommand, Result<CadastrarContatoResult>>
{
    public async Task<Result<CadastrarContatoResult>> Handle(
        CadastrarContatoCommand command, CancellationToken cancellationToken)
    {
        var registros = await repositorioContato.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Nome.Equals(command.Nome)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um contato registrado com este nome."));

        try
        {
            var contato = new Contato(
                command.Nome,
                command.Telefone,
                command.Email,
                command.Empresa,
                command.Cargo
            );

            await repositorioContato.CadastrarAsync(contato);

            await unitOfWork.CommitAsync();

            var result = new CadastrarContatoResult(contato.Id);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante o registro de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
