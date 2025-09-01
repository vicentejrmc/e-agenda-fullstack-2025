using eAgenda.Core.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloContato;

public class MapeadorContatoEmOrm : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .IsRequired();

        builder.Property(x => x.Empresa)
            .IsRequired();

        builder.Property(x => x.Cargo)
            .IsRequired();

        builder.HasMany(x => x.Compromissos)
            .WithOne(c => c.Contato)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
