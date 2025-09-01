using eAgenda.Core.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa;

public class RepositorioTarefaEmOrm : IRepositorioTarefa
{
    private readonly DbSet<Tarefa> registros;

    public RepositorioTarefaEmOrm(AppDbContext dbContext)
    {
        registros = dbContext.Set<Tarefa>();
    }

    public async Task CadastrarAsync(Tarefa tarefa)
    {
        await registros.AddAsync(tarefa);
    }

    public async Task<bool> EditarAsync(Guid idTarefa, Tarefa tarefaEditada)
    {
        var registroSelecionado = await SelecionarTarefaPorIdAsync(idTarefa);

        if (registroSelecionado is null)
            return false;

        registroSelecionado.AtualizarRegistro(tarefaEditada);

        return true;
    }

    public async Task<bool> ExcluirAsync(Guid idTarefa)
    {
        var registroSelecionado = await SelecionarTarefaPorIdAsync(idTarefa);

        if (registroSelecionado is null)
            return false;

        registros.Remove(registroSelecionado);

        return true;
    }

    public async Task<Tarefa?> SelecionarTarefaPorIdAsync(Guid idTarefa)
    {
        return await registros
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id.Equals(idTarefa));
    }

    public async Task<List<Tarefa>> SelecionarTarefasAsync()
    {
        return await registros
            .Include(x => x.Itens)
            .ToListAsync();
    }

    public async Task<List<Tarefa>> SelecionarTarefasConcluidasAsync()
    {
        return await registros
            .Where(x => x.Concluida)
            .Include(x => x.Itens)
            .ToListAsync();
    }

    public async Task<List<Tarefa>> SelecionarTarefasPendentesAsync()
    {
        return await registros
            .Where(x => !x.Concluida)
            .Include(x => x.Itens)
            .ToListAsync();
    }
}
