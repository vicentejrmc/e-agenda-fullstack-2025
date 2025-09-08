using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;
public class SelecionarContatosPorIdQueryHandler(
    IMapper mapper,
    IRepositorioContato repositorioContato,
    ILogger<SelecionarContatosPorIdQueryHandler> logger
) : IRequestHandler<SelecionarContatosPorIdQuery, Result<SelecionarContatosPorIdResult>>
{
    public async Task<Result<SelecionarContatosPorIdResult>> Handle(SelecionarContatosPorIdQuery query, CancellationToken cancellationToken)
    {
        try 
        {
            var resgistro = await repositorioContato.SelecionarRegistroPorIdAsync(query.Id); 

            if (resgistro == null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(query.Id));


            //Logica usada sem o AutoMapper
            //var result = new SelecionarContatosPorIdResult(
            //    resgistro.Id,
            //    resgistro.Nome,
            //    resgistro.Telefone,
            //    resgistro.Email,
            //    resgistro.Empresa,
            //    resgistro.Cargo,
            //    resgistro.Compromissos.Select(r => new DetalhesComprimissoConatatoDto(
            //        r.Assunto,
            //        r.Data,
            //        r.HoraInicio,
            //        r.HoraTermino
            //    )).ToImmutableList()
            //);

            var result = mapper.Map<SelecionarContatosPorIdResult>(resgistro);

            return Result.Ok(result);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro durante a seleção do {@Registro}.", query);

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
