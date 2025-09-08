using eAgenda.Core.Aplicacao.ModuloContato.Cadastrar;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using eAgenda.WebApi.Models.ModuloContato;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("contatos")]
public class ContatoController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarContatoResult>> Cadastrar(CadastrarContatoRequest request)
    {
        var command = new CadastrarContatoCommand(
             request.Nome,
             request.Telefone,
             request.Email,
             request.Empresa,
             request.Cargo
        );

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = new CadastrarContatoResponse(result.Value.Id);

        return Created(string.Empty, response);
    }

    [HttpPut("{id:guid}")] // HttpPut alterar um recurso existente na API
    public async Task<ActionResult> Editar(Guid id, EditarContatoRequest request)
    {
        // junta os dados do id da url com o corpo da requisição
        var command = new EditarContatoCommand(
            id,
            request.Nome,
            request.Telefone,
            request.Email,
            request.Empresa,
            request.Cargo
        );

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        // retorna os dados editados
        var response = new EditarContatoResponse(
            result.Value.Nome,
            result.Value.Telefone,
            result.Value.Email,
            result.Value.Empresa,
            result.Value.Cargo
        );

        return Ok(response);
    }

    [HttpDelete("{id:guid}")] // HttpDelete para deletar um recurso existente na API
    public async Task<ActionResult<ExcluirContatoResponse>> Excluir(Guid id)
    {
        var command = new ExcluirContatoCommand(id);

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistros(
    [FromQuery] SelecionarContatosRequest? request
)
    {
        var query = new SelecionarContatosQuery(request?.Quantidade);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return BadRequest();

        var response = new SelecionarContatosResponse(
            result.Value.Contato.Count,
            result.Value.Contato
        );

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarContatosPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = new SelecionarContatosPorIdQuery(id);

        var result = await mediator.Send(query);

        if(result.IsFailed)
            return NotFound(id);

        var response = new SelecionarContatosPorIdResponse(
            result.Value.Id,
            result.Value.Nome,
            result.Value.Telefone,
            result.Value.Email,
            result.Value.Empresa,
            result.Value.Cargo,
            result.Value.Compromissos
            );

        return Ok(response);
    }
}
