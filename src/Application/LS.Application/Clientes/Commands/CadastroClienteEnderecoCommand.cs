using FluentValidation;
using LS.Domain.Core.Messages;
using System;

namespace LS.Application.Clientes.Commands
{
    public class CadastroClienteEnderecoCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public CadastroClienteEnderecoCommand(Guid clienteId, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
        {
            AggregateId = clienteId;
            ClienteId = clienteId;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastroClienteEnderecoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class CadastroClienteEnderecoValidation : AbstractValidator<CadastroClienteEnderecoCommand>
        {
            public CadastroClienteEnderecoValidation()
            {
                RuleFor(c => c.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Informe o Id do Cliente");

                RuleFor(c => c.Logradouro)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro");

                RuleFor(c => c.Numero)
                    .NotEmpty()
                    .WithMessage("Informe o Número");

                RuleFor(c => c.Cep)
                    .NotEmpty()
                    .WithMessage("Informe o CEP");

                RuleFor(c => c.Bairro)
                    .NotEmpty()
                    .WithMessage("Informe o Bairro");

                RuleFor(c => c.Cidade)
                    .NotEmpty()
                    .WithMessage("Informe o Cidade");

                RuleFor(c => c.Estado)
                    .NotEmpty()
                    .WithMessage("Informe o Estado");
            }
        }
    }
}
