using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloCategoria;
using System.Diagnostics.CodeAnalysis;

namespace eAgenda.Core.Dominio.ModuloDespesa;

public class Despesa : EntidadeBase<Despesa>
{
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataOcorencia { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();

    [ExcludeFromCodeCoverage]
    public Despesa() { }

    public Despesa(string descricao, decimal valor, DateTime data, FormaPagamento formaPagamento) : this()
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        DataOcorencia = data;
        FormaPagamento = formaPagamento;
    }

    public void RegistarCategoria(Categoria categoria)
    {
        if (Categorias.Contains(categoria))
            return;

        Categorias.Add(categoria);

        categoria.RegistrarDespesa(this);
    }

    public void RemoverCategoria(Categoria categoria)
    {
        if (!Categorias.Contains(categoria))
            return;

        Categorias.Remove(categoria);

        categoria.RemoverDespesa(this);
    }

    public override void AtualizarRegistro(Despesa registroEditado)
    {
        Descricao = registroEditado.Descricao;
        Valor = registroEditado.Valor;
        DataOcorencia = registroEditado.DataOcorencia;
        FormaPagamento = registroEditado.FormaPagamento;
    }
}