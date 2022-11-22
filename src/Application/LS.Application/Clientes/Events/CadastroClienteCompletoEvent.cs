using LS.Domain.Core.Messages;
using System;

namespace LS.Application.Clientes.Events
{
    public class CadastroClienteCompletoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Celular { get; private set; }
        public string Profissao { get; private set; }
        public string Sexo { get; private set; }

        public CadastroClienteCompletoEvent(
            Guid clienteId, string nome, string sobrenome, 
            DateTime dataNascimento, string cpf, string email, 
            bool ativo, DateTime dataCadastro, string celular,
            string profissao, string sexo)
        {
            AggregateId = clienteId;
            ClienteId = clienteId;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
            Celular = celular;
            Profissao = profissao;
            Sexo = sexo;
        }
    }
}
