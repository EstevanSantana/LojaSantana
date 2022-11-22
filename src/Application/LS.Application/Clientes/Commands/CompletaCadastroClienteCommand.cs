using FluentValidation;
using LS.Domain.Clientes.Enum;
using LS.Domain.Core.Messages;
using System;

namespace LS.Application.Clientes.Commands
{
    public class CompletaCadastroClienteCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Celular { get; private set; }
        public string Profissao { get; private set; }
        public ESexo Sexo { get; private set; }

        public CompletaCadastroClienteCommand(Guid clienteId, DateTime dataNascimento, string celular, string profissao, ESexo sexo)
        {
            AggregateId = clienteId;
            ClienteId = clienteId;
            DataNascimento = dataNascimento;
            Celular = celular;
            Profissao = profissao;
            Sexo = sexo;
        }

        public override bool EhValido()
        {
            ValidationResult = new CompletarCadastroClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class CompletarCadastroClienteValidation : AbstractValidator<CompletaCadastroClienteCommand>
        {
            public CompletarCadastroClienteValidation()
            {
                RuleFor(c => c.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Informe o Id do Cliente");

                RuleFor(c => c.DataNascimento)
                    .NotEmpty().Must(c => !(c == DateTime.MinValue))
                    .WithMessage("Informe a Data de Nascimento");

                RuleFor(c => c.Celular)
                    .NotEmpty().WithMessage("Informe o Celular")
                    .MinimumLength(8).WithMessage("Numero de Celular deve estar entre 8 e 15 caracteres.")
                    .MaximumLength(20).WithMessage("Numero de Celular deve estar entre 8 e 15 caracteres.");

                RuleFor(c => c.Sexo)
                    .Must(ValidaSexo)
                    .WithMessage("Informe o Sexo");

                RuleFor(c => c.Profissao)
                    .NotEmpty().WithMessage("Informe a Profissao")
                    .MinimumLength(2).WithMessage("Profissao deve estar entre 2 e 50 caracteres.")
                    .MaximumLength(50).WithMessage("Profissao deve estar entre 2 e 50 caracteres.");
            }

            public static bool ValidaSexo(ESexo sexo) => sexo != ESexo.NaoInformado;
        }
    }
}
