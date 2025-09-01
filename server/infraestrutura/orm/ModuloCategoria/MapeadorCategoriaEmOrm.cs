using eAgenda.Core.Dominio.ModuloCategoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloCategoria;

public class MapeadorCategoriaEmOrm : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Titulo)
            .IsRequired();

        builder.HasMany(x => x.Despesas)
            .WithMany(c => c.Categorias);

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
