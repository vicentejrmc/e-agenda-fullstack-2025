using eAgenda.Core.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.MapeadorDespesa;

public class RepositorioDespesaEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Despesa>(contexto), IRepositorioDespesa;