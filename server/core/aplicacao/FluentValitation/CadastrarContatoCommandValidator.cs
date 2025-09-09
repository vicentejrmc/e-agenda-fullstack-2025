using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using FluentValidation;

namespace eAgenda.Core.Aplicacao.FluentValitation;
public class CadastrarContatoCommandValidator : AbstractValidator<CadastrarContatoCommand>
{
    public CadastrarContatoCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é Obrigatório.")
            .MinimumLength(2).WithMessage("O nome deve ter no minimo {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome não pode conter mais de {MaxLength} caracteres.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatorio.")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage("O telefone deve seguir o formato (00) 0000-0000 ou (00) 00000-0000.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve seguir o formato nome@provedor.");

        RuleFor(x => x.Empresa)
            .MinimumLength(2).WithMessage("O nome da empresa deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome da empresa deve conter no máximo {MaxLength} caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Empresa));
            //When indica que a validação se trata de um campo opcional.
            //A Validação só aplicada se algo for digitado no campo

        RuleFor(x => x.Cargo)
            .MinimumLength(2).WithMessage("O cargo deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O cargo deve conter no máximo {MaxLength} caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Cargo));
    }
}
