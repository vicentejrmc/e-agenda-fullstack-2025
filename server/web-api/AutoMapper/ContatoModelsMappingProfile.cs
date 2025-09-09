using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.WebApi.Models.ModuloContato;
using System.Collections.Immutable;

namespace eAgenda.WebApi.AutoMapper;

public class ContatoModelsMappingProfile : Profile
{
    public ContatoModelsMappingProfile()
    {
        //Mapeador Cadastro.
        CreateMap<CadastrarContatoRequest, CadastrarContatoCommand>();
        CreateMap<CadastrarContatoResult, CadastrarContatoResponse>();

        //Mapeador Edição.
        CreateMap<(Guid, EditarContatoRequest), EditarContatoCommand>()
            .ConvertUsing(src => new EditarContatoCommand(
                src.Item1,
                src.Item2.Nome,
                src.Item2.Telefone,
                src.Item2.Email,
                src.Item2.Empresa,
                src.Item2.Cargo
            ));
        CreateMap<EditarContatoResult, EditarContatoResponse>();

        //Mapeador Exclusão.
        CreateMap<Guid, ExcluirContatoCommand>()
            .ConstructUsing(src => new ExcluirContatoCommand(src));

        //Mapeador Selecionar.
        CreateMap<SelecionarContatosRequest, SelecionarContatosQuery>();
        CreateMap<SelecionarContatosResult, SelecionarContatosResponse>()
           .ConvertUsing((src, dest, ctx)=> new SelecionarContatosResponse(
               src.Contatos.Count,
               src.Contatos.Select(c => ctx.Mapper.Map<SelecionarContatosDto>(c)).ToImmutableList() ?? 
               ImmutableList<SelecionarContatosDto>.Empty
               ));

        //Mapeador SelecionarPorId.
        CreateMap<Guid, SelecionarContatosPorIdQuery>()
            .ConvertUsing(src => new SelecionarContatosPorIdQuery(src));

        CreateMap<SelecionarContatosPorIdResult, SelecionarContatosPorIdResponse>()
            .ConvertUsing(src => new SelecionarContatosPorIdResponse(
                src.Id,
                src.Nome,
                src.Telefone,
                src.Email,
                src.Empresa,
                src.Cargo,
                (src.Compromissos ?? ImmutableList.Create<DetalhesComprimissoConatatoDto>())
                    .Select(r => new DetalhesComprimissoConatatoDto(
                        r.Assunto,
                        r.Data,
                        r.HoraInicio,
                        r.HoraTermino
                    )).ToImmutableList()
                ));
    }
}
