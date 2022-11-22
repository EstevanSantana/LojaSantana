using LS.Domain.Core.Messages;
using System;

namespace LS.Application.Clientes.Events
{
    public class ClienteCadastradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ClienteCadastradoEvent(
            Guid id, string nome, string sobrenome, 
            string cpf, string email, DateTime dataCadastro)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Email = email;
            DataCadastro = dataCadastro;
        }
    }
}
