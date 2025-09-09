using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.WebApi.Models.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("contatos")]
public class ContatoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarContatoResult>> Cadastrar(CadastrarContatoRequest request)
    {
        var command = mapper.Map<CadastrarContatoCommand>(request);

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CadastrarContatoResponse>(result.Value); 
        // Dê atenção ao .Value para que o mapeamento pegue a referencia correda do ContatoResult que esta embrulada no result

        return Created(string.Empty, response);
    }

    [HttpPut("{id:guid}")] // HttpPut alterar um recurso existente na API
    public async Task<ActionResult> Editar(Guid id, EditarContatoRequest request)
    {
        // Tupla, objeto formado por dois argumentos                           //argumentos do que será passado
        var command = mapper.Map<(Guid, EditarContatoRequest), EditarContatoCommand>((id, request));

        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            if(result.HasError(e => e.HasMetadata("TipoErro", m => m.Equals("RequisicaoInvalida"))))
            {
                var errosDeValidacao = result.Errors
                    .SelectMany(e => e.Reasons.OfType<IError>())
                    .Select(e => e.Message);

                return BadRequest(errosDeValidacao);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var response = mapper.Map<EditarContatoResponse>(result.Value);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")] // HttpDelete para deletar um recurso existente na API
    public async Task<ActionResult<ExcluirContatoResponse>> Excluir(Guid id)
    {
        var command = mapper.Map<ExcluirContatoCommand>(id);

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistros(
    [FromQuery] SelecionarContatosRequest? request)
    {
        var query = mapper.Map<SelecionarContatosQuery>(request);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<SelecionarContatosResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarContatosPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = mapper.Map<SelecionarContatosPorIdQuery>(id);

        var result = await mediator.Send(query);

        if(result.IsFailed)
            return NotFound(id);

        var response = mapper.Map<SelecionarContatosPorIdResult>(result.Value);

        return Ok(response);
    }
}
