using eAgenda.Core.Dominio.ModuloContato;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{
    private readonly IRepositorioContato repositorioContato;
    private readonly ILogger<ContatoController> _logger;

    public ContatoController(
        IRepositorioContato repositorioContato,
        ILogger<ContatoController> logger
    )
    {
        this.repositorioContato = repositorioContato;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> SelecionarRegistros()
    {
        var registros = await repositorioContato.SelecionarRegistrosAsync();

        return Ok(registros);
    }
}
