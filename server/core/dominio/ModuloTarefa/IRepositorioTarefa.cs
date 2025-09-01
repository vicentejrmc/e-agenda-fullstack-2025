namespace eAgenda.Core.Dominio.ModuloTarefa;

public interface IRepositorioTarefa
{
    public Task CadastrarAsync(Tarefa tarefa);
    public Task<bool> EditarAsync(Guid idTarefa, Tarefa tarefaEditada);
    public Task<bool> ExcluirAsync(Guid idTarefa);
    public Task<List<Tarefa>> SelecionarTarefasAsync();
    public Task<List<Tarefa>> SelecionarTarefasPendentesAsync();
    public Task<List<Tarefa>> SelecionarTarefasConcluidasAsync();
    public Task<Tarefa?> SelecionarTarefaPorIdAsync(Guid idTarefa);
};