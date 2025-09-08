using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloCompromisso;
using eAgenda.Core.Dominio.ModuloContato;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.AutoMapper;
public class ContatoMappingProfile : Profile
{
    public ContatoMappingProfile()
    {
        CreateMap<CadastrarContatoCommand, Contato>();
        CreateMap<Contato, CadastrarContatoResult>();

        CreateMap<EditarContatoCommand, Contato>();
        CreateMap<Contato, EditarContatoResult>();

        // Para Selecionar o contato por Id é necessario trazer a logica para o AutoMapper,
        // isso é necessario por conta dos DetalhesCompromisso.
        CreateMap<Contato, SelecionarContatosPorIdResult>()
            .ConstructUsing(src => new SelecionarContatosPorIdResult(
                src.Id,
                src.Nome,
                src.Telefone,
                src.Email,
                src.Empresa,
                src.Cargo,
                (src.Compromissos ?? Enumerable.Empty<Compromisso>()) //caso seja null retorna uma lista vazia.
                    .Select(r => new DetalhesComprimissoConatatoDto(
                        r.Assunto,
                        r.Data,
                        r.HoraInicio,
                        r.HoraTermino
                    )).ToImmutableList()
            ));

        CreateMap<IEnumerable<Contato>, SelecionarContatosResult>()
            .ConvertUsing((src, dest, ctx) =>
                new SelecionarContatosResult(src?.Select(c => ctx.Mapper.Map<SelecionarContatosDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarContatosDto>.Empty
                )
            ); 
    }
}
