using FluentValidation;
using FluentValidation.Results;
using LS.Domain.Core.Messages;

namespace LS.Application.Clientes.Commands
{
    public class CadastroClienteCommand : Command
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public CadastroClienteCommand(string nome, string sobrenome, string cpf, string email)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Email = email;
        }

        public override bool EhValido()
        {
            ValidationResult = new ClienteCadastroValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class ClienteCadastroValidation : AbstractValidator<CadastroClienteCommand>
        {
            public ClienteCadastroValidation()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O Nome deve ser informado.")
                    .MinimumLength(2).WithMessage("Nome deve estar entre 2 e 50 caracteres.")
                    .MaximumLength(50).WithMessage("Nome deve estar entre 2 e 50 caracteres.");

                RuleFor(c => c.Sobrenome)
                    .NotEmpty().WithMessage("O Sobrenome deve ser informado.")
                    .MinimumLength(2).WithMessage("Sobrenome deve estar entre 2 e 50 caracteres.")
                    .MaximumLength(50).WithMessage("Sobrenome deve estar entre 2 e 50 caracteres.");

                RuleFor(c => c.Email)
                    .Must(HasValidarEmail)
                    .WithMessage("O Email informado não é valido.");

                RuleFor(c => c.Cpf)
                    .Must(HasValidarCpf)
                    .WithMessage("O CPF informado não é valido.");
            }

            protected static bool HasValidarEmail(string email)
            {
                return Domain.Core.DomainObjects.Email.Validar(email);
            }

            protected static bool HasValidarCpf(string cpf)
            {
                return Domain.Core.DomainObjects.Cpf.Validar(cpf);
            }
        }
    }
}
