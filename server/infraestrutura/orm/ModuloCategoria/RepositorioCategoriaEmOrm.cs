using eAgenda.Core.Dominio.ModuloCategoria;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.ModuloCategoria;

public class RepositorioCategoriaEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Categoria>(contexto), IRepositorioCategoria;