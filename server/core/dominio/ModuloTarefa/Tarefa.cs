using eAgenda.Core.Dominio.Compartilhado;
using System.Diagnostics.CodeAnalysis;

namespace eAgenda.Core.Dominio.ModuloTarefa;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public PrioridadeTarefa Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public bool Concluida { get; set; }

    public decimal PercentualConcluido
    {
        get
        {
            if (Itens.Count == 0)
                return default;

            int qtdConcluidos = Itens.Count(i => i.Concluido);

            decimal percentualBase = qtdConcluidos / (decimal)Itens.Count * 100;

            return Math.Round(percentualBase, 2);
        }
    }

    public List<ItemTarefa> Itens { get; set; } = new List<ItemTarefa>();


    [ExcludeFromCodeCoverage]
    public Tarefa() { }

    public Tarefa(string titulo, PrioridadeTarefa prioridade) : this()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.Now;
        Concluida = false;

        Titulo = titulo;
        Prioridade = prioridade;
    }

    public void Concluir()
    {
        Concluida = true;
        DataConclusao = DateTime.Now;
    }

    public void MarcarPendente()
    {
        Concluida = false;
        DataConclusao = null;
    }

    public ItemTarefa? ObterItem(Guid idItem)
    {
        return Itens.Find(i => i.Id.Equals(idItem));
    }

    public ItemTarefa AdicionarItem(string titulo)
    {
        var item = new ItemTarefa(titulo, this);

        Itens.Add(item);

        MarcarPendente();

        return item;
    }

    public ItemTarefa AdicionarItem(ItemTarefa item)
    {
        Itens.Add(item);

        return item;
    }

    public bool RemoverItem(ItemTarefa item)
    {
        Itens.Remove(item);

        MarcarPendente();

        return true;
    }

    public void ConcluirItem(ItemTarefa item)
    {
        item.Concluir();
    }

    public void MarcarItemPendente(ItemTarefa item)
    {
        item.MarcarPendente();

        MarcarPendente();
    }

    public override void AtualizarRegistro(Tarefa registroEditado)
    {
        Titulo = registroEditado.Titulo;
        Prioridade = registroEditado.Prioridade;
    }
}