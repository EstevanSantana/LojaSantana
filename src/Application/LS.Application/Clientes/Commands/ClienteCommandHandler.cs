using FluentValidation.Results;
using LS.Application.Clientes.Events;
using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using LS.Domain.Core.Messages;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LS.Application.Clientes.Commands
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<CadastroClienteCommand, ValidationResult>,
        IRequestHandler<CompletaCadastroClienteCommand, ValidationResult>,
        IRequestHandler<CadastroClienteEnderecoCommand, ValidationResult>

    {
        private readonly IClienteWriteRepository _clienteWriteRepository;

        public ClienteCommandHandler(IClienteWriteRepository clienteWriteRepository)
        {
            _clienteWriteRepository = clienteWriteRepository;
        }

        public async Task<ValidationResult> Handle(CadastroClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente =
                Cliente.Factory.CriarCadastroCliente(
                    message.Nome, message.Sobrenome, message.Email, message.Cpf);

            if (await ObterClientePorId(message.Cpf) != null)
            {
                AdicionarErro("O cpf já está em uso.");
                return ValidationResult;
            }

            _clienteWriteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(
                new ClienteCadastradoEvent(
                    cliente.Id, message.Nome, 
                    message.Sobrenome, cliente.Cpf.Numero,
                    message.Email, cliente.DataCadastro));

            try
            {
                return await PersistirDados(_clienteWriteRepository.UnitOfWork);
            }
            catch
            {
                _clienteWriteRepository.ApagarCliente(cliente);
                AdicionarErro($"Não foi possivel fazer o cadastro do cliente {cliente.ObterNomeCompletoCliente()}.");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(CompletaCadastroClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = await ObterClientePorId(message.ClienteId);
            if (cliente == null)
            {
                AdicionarErro("O Id informado não existe.");
                return ValidationResult;
            }

            cliente.CompletarCadastroCliente(
                message.ClienteId, message.DataNascimento,
                message.Sexo, message.Profissao, message.Celular);

            _clienteWriteRepository.AtualizarCliente(cliente);

            cliente.AdicionarEvento(
                new CadastroClienteCompletoEvent(
                    cliente.Id, cliente.Nome, cliente.Sobrenome,
                    cliente.DataNascimento, cliente.Cpf.Numero, 
                    cliente.Email.Endereco, cliente.Ativo, cliente.DataCadastro, 
                    cliente.Celular, cliente.Profissao, cliente.Sexo));

            try
            {
                return await PersistirDados(_clienteWriteRepository.UnitOfWork);
            }
            catch
            {
                AdicionarErro($"Não foi possivel completar o cadastro do cliente {cliente.ObterNomeCompletoCliente()}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(CadastroClienteEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = await ObterClientePorId(message.ClienteId);
            if (cliente == null)
            {
                AdicionarErro("O Id informado não existe.");
                return ValidationResult;
            }

            cliente.AdicionarEndereco(
                message.Logradouro, message.Numero, 
                message.Complemento, message.Bairro,
                message.Cep, message.Cidade, message.Estado);

            _clienteWriteRepository.AdicionarEndereco(cliente.Endereco);

            cliente.AdicionarEvento(
                new ClienteEnderecoCadastradoEvent(
                    cliente.Id, cliente.Nome, cliente.Sobrenome,
                    cliente.DataNascimento, cliente.Cpf.Numero,
                    cliente.Email.Endereco, cliente.Ativo, cliente.DataCadastro, 
                    cliente.Celular, cliente.Profissao, cliente.Sexo, 
                    cliente.Endereco.Id, cliente.Endereco.Logradouro,
                    cliente.Endereco.Numero, cliente.Endereco.Complemento, 
                    cliente.Endereco.Bairro, cliente.Endereco.Cep,
                    cliente.Endereco.Cidade, cliente.Endereco.Estado));

            try
            {
                return await PersistirDados(_clienteWriteRepository.UnitOfWork);
            }
            catch(Exception ex)
            {
                AdicionarErro($"Não foi possivel adicionar o endereco do {cliente.ObterNomeCompletoCliente()}");
                return ValidationResult;
            }
        }

        private async Task<Cliente> ObterClientePorId(string cpf)
            => await _clienteWriteRepository.ObterPorCpf(cpf);
        
        private async Task<Cliente> ObterClientePorId(Guid clienteId)
            => await _clienteWriteRepository.ObterPorId(clienteId);
        
    }
}
