using eAgenda.Core.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.ModuloContato;

public class RepositorioContatoEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Contato>(contexto), IRepositorioContato;
