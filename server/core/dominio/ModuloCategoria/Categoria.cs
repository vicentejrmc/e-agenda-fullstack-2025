using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloDespesa;
using System.Diagnostics.CodeAnalysis;

namespace eAgenda.Core.Dominio.ModuloCategoria;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; }
    public List<Despesa> Despesas { get; set; } = new List<Despesa>();

    [ExcludeFromCodeCoverage]
    public Categoria() { }

    public Categoria(string titulo) : this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
    }

    public override void AtualizarRegistro(Categoria registroEditado)
    {
        Titulo = registroEditado.Titulo;
    }


    public void RegistrarDespesa(Despesa Despesa)
    {
        if (Despesas.Any(c => c.Id == Despesa.Id))
            return;

        Despesas.Add(Despesa);
    }

    public void RemoverDespesa(Despesa Despesa)
    {
        if (!Despesas.Any(c => c.Id == Despesa.Id))
            return;

        Despesas.Remove(Despesa);
    }
}