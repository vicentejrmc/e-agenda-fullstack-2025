using eAgenda.Core.Aplicacao.ModuloContato.Cadastrar;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using eAgenda.WebApi.Models.ModuloContato;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IRepositorioContato repositorioContato;
    private readonly ILogger<ContatoController> logger;

    public ContatoController(
        IMediator mediator,
        IRepositorioContato repositorioContato,
        ILogger<ContatoController> logger
    )
    {
        this.mediator = mediator;
        this.repositorioContato = repositorioContato;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(CadastrarContatoRequest request)
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
    public async Task<IActionResult> SelecionarRegistros(
        [FromQuery] SelecionarContatosRequest? request
    )
    {
        var registros = await repositorioContato.SelecionarRegistrosAsync();

        return Ok(registros);
    }
}
