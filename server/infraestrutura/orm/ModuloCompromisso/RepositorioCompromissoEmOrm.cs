using eAgenda.Core.Dominio.ModuloCompromisso;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.ModuloCompromisso;

public class RepositorioCompromissoEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Compromisso>(contexto), IRepositorioCompromisso;