using LS.Domain.Core.DomainObjects;
using LS.Domain.Clientes.Enum;
using System;

namespace LS.Domain.Clientes.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public bool Ativo { get; private set; } = false;
        public DateTime DataCadastro { get; private set; }
        public string Celular { get; private set; }
        public string Profissao { get; private set; }
        public string Sexo { get; private set; }

        // EF
        public Endereco Endereco { get; private set; }
        
        #region Construtores
        private Cliente(
            Guid id, 
            string nome, 
            string sobrenome,
            string email, 
            string cpf)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            DataCadastro = DateTime.Now;
        }

        private Cliente(
            Guid id, 
            string nome, 
            string sobrenome,
            DateTime dataNascimento,
            string cpf,
            string email,
            bool ativo, 
            DateTime dataCadastro, 
            string celular,
            string profissao, 
            string sexo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Ativo = ativo;
            DataCadastro = dataCadastro;
            Celular = celular;
            Profissao = profissao;
            Sexo = sexo;
        }
        
        private Cliente(Guid id, string nome, string sobrenome, DateTime dataNascimento, string cpf, string email, 
            bool ativo, DateTime dataCadastro, string celular, string profissao, string sexo, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Ativo = ativo;
            DataCadastro = dataCadastro;
            Celular = celular;
            Profissao = profissao;
            Sexo = sexo;
        }

        // Construtor EF
        protected Cliente() { }
        #endregion

        internal void AtivarCliente() => Ativo = true;

        internal static bool DataNacimentoEhValida(DateTime data) => data <= DateTime.Now.AddYears(-18);

        internal static string MontarSexo(ESexo sexo)
        {
            return sexo switch
            {
                ESexo.NaoInformado 
                    => ESexo.NaoInformado.ToString(),

                ESexo.Masculino 
                    => ESexo.Masculino.ToString(),

                ESexo.Feminino 
                    => ESexo.Masculino.ToString(),

                _ => throw new DomainException("Tipo de Enum não suportado."),
            };
        }

        public string MontarAtivo() => Ativo ? "Ativo" : "Não Ativo";

        public string ObterNomeCompletoCliente() => string.Format($"{Nome} {Sobrenome}");

        public void AdicionarEndereco(
            string logradouro, 
            string numero, 
            string complemento, 
            string bairro,
            string cep, 
            string cidade,
            string estado
            ) => Endereco = 
                Endereco.Factory.CriarEnderecoCompletarCadastroCliente(
                    logradouro, numero, complemento, bairro, cep, cidade, estado, Id);

        public void CompletarCadastroCliente(
            Guid clienteId, 
            DateTime dataNascimento,
            ESexo sexo, 
            string profissao,
            string celular)
        {
            if (clienteId == Guid.Empty) 
                throw new DomainException("O ClienteId não pode estar vazio.");

            if (sexo == ESexo.NaoInformado) 
                throw new DomainException("O Sexo deve ser informado.");

            if (string.IsNullOrEmpty(profissao)) 
                throw new DomainException("A Profissao não pode estar vazia.");

            if (string.IsNullOrEmpty(celular)) 
                throw new DomainException("O Celular não pode estar vazio.");

            if (!DataNacimentoEhValida(dataNascimento)) 
                throw new DomainException("A Idade não pode ser menor que 18 anos.");

            Id = clienteId;
            DataNascimento = dataNascimento;
            Sexo = MontarSexo(sexo);
            Profissao = profissao;
            Celular = celular;

            AtivarCliente();
        }

        #region Fabrica
        public static class Factory
        {
            public static Cliente CriarCadastroCliente(
                string nome, 
                string sobrenome, 
                string email, 
                string cpf)
            {
                if (string.IsNullOrEmpty(nome)) 
                    throw new DomainException("O Nome não pode ser vazio.");

                if (string.IsNullOrEmpty(sobrenome)) 
                    throw new DomainException("O Sobrenome não pode ser vazio.");

                if (string.IsNullOrEmpty(email)) 
                    throw new DomainException("O Email não pode ser vazio.");

                if (string.IsNullOrEmpty(cpf)) 
                    throw new DomainException("O CPF não pode ser vazio.");

                return new Cliente(Guid.NewGuid(), nome, sobrenome, email, cpf);
            }

            public static Cliente CriarClienteCadastradoEvento(
                Guid clienteId, 
                string nome, 
                string sobrenome,
                string email, 
                string cpf
                ) => new Cliente(
                    clienteId, nome, sobrenome, email, cpf);

            public static Cliente CriarCadastroClienteCompletoEvento(
                Guid clienteId, 
                string nome, 
                string sobrenome, 
                DateTime dataNascimento, 
                string cpf,
                string email, 
                bool ativo, 
                DateTime dataCadastro, 
                string celular, 
                string profissao, 
                string sexo
                ) => new Cliente(
                    clienteId, nome, sobrenome, 
                    dataNascimento, cpf, email, ativo, 
                    dataCadastro, celular, profissao, sexo);

            public static Cliente CriarClienteEnderecoCadastradoEvento(
                Guid clienteId, 
                string nome,
                string sobrenome, 
                DateTime dataNascimento, 
                string cpf,
                string email,
                bool ativo, 
                DateTime dataCadastro, 
                string celular, 
                string profissao,
                string sexo, 
                Guid enderecoId, 
                string logradouro,
                string numero, 
                string complemento, 
                string bairro, 
                string cep, 
                string cidade, 
                string estado)
            {
                var endereco = 
                    Endereco.Factory.CriarEnderecoClienteEnderecoCadastradoEvento(
                    enderecoId, logradouro, numero, complemento, 
                    bairro, cep, cidade, estado, clienteId);

                return new Cliente(
                    clienteId, nome, sobrenome, dataNascimento, cpf, email, 
                    ativo, dataCadastro, celular, profissao, sexo, endereco);
            }
        }
        #endregion
    }
}
