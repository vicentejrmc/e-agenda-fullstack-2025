using eAgenda.Core.Dominio.ModuloTarefa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa;

public class MapeadorTarefaEmOrm : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.Property(x => x.Id)
           .ValueGeneratedNever()
           .IsRequired();

        builder.Property(x => x.Titulo)
            .IsRequired();

        builder.Property(x => x.Prioridade)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Property(x => x.DataConclusao)
            .IsRequired();

        builder.Property(x => x.Concluida)
            .IsRequired();

        builder.Ignore(x => x.PercentualConcluido);

        builder.HasMany(x => x.Itens)
            .WithOne(i => i.Tarefa);

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}