using LS.Domain.Core.DomainObjects;
using System;

namespace LS.Domain.Clientes.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid ClienteId { get; private set; }

        // EF
        public Cliente Cliente { get; protected set; }

        #region Construtor
        private Endereco(
            Guid id, 
            string logradouro,
            string numero, 
            string complemento, 
            string bairro, 
            string cep, 
            string cidade,
            string estado,
            Guid clienteId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            ClienteId = clienteId;
        }

        // Construtor EF
        protected Endereco() { }
        #endregion

        #region Fabrica
        public static class Factory
        {
            public static Endereco CriarEnderecoCompletarCadastroCliente(
                string logradouro, 
                string numero, 
                string complemento,
                string bairro, 
                string cep, 
                string cidade, 
                string estado,
                Guid clienteId)
            {
                if (clienteId == Guid.Empty) 
                    throw new DomainException("O Id do Cliente não pode ser vazio.");

                if (string.IsNullOrEmpty(logradouro)) 
                    throw new DomainException("O Logradouro não pode ser vazio.");

                if (string.IsNullOrEmpty(numero)) 
                    throw new DomainException("O Numero não pode ser vazio.");

                if (string.IsNullOrEmpty(cep)) 
                    throw new DomainException("O CEP não pode ser vazio.");

                if (string.IsNullOrEmpty(cidade))
                    throw new DomainException("A Cidade não pode ser vazio.");

                if (string.IsNullOrEmpty(estado)) 
                    throw new DomainException("O Estado não pode ser vazio.");

                return new Endereco(
                    Guid.NewGuid(),
                    logradouro, 
                    numero, 
                    complemento, 
                    bairro, 
                    cep,
                    cidade,
                    estado, 
                    clienteId);
            }

            public static Endereco CriarEnderecoClienteEnderecoCadastradoEvento(
                Guid enderecoId, 
                string logradouro,
                string numero, 
                string complemento, 
                string bairro,
                string cep, 
                string cidade,
                string estado, 
                Guid clienteId
                ) => new Endereco(
                    enderecoId, 
                    logradouro,
                    numero,
                    complemento,
                    bairro,
                    cep,
                    cidade, 
                    estado, 
                    clienteId);
        }
        #endregion
    }
}
