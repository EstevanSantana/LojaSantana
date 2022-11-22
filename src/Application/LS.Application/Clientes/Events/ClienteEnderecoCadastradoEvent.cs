using LS.Domain.Core.Messages;
using System;

namespace LS.Application.Clientes.Events
{
    public class ClienteEnderecoCadastradoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid EnderecoId { get; private set; }
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
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public ClienteEnderecoCadastradoEvent(
            Guid clienteId, string nome, string sobrenome,
            DateTime dataNascimento, string cpf, string email, 
            bool ativo, DateTime dataCadastro, string celular, 
            string profissao, string sexo, Guid enderecoId,
            string logradouro, string numero, string complemento,
            string bairro, string cep, string cidade, string estado)
        {
            ClienteId = clienteId;
            EnderecoId = enderecoId;
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
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }
    }
}
