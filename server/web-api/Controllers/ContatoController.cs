using eAgenda.Core.Aplicacao.ModuloContato.Cadastrar;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using eAgenda.WebApi.Models.ModuloContato;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistros(
        [FromQuery] SelecionarContatosRequest? request
    )
    {
        var query = new SelecionarContatosQuery(request?.Quantidade);

        var result = await mediator.Send(query);

        if(result.IsFailed)
            return BadRequest();

        var response = new SelecionarContatosResponse(
            result.Value.Contato.Count,
            result.Value.Contato
        );

        return Ok(response);
    }
}
