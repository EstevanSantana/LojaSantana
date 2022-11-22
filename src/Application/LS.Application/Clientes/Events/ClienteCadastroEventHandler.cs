using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using LS.Domain.Core.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LS.Application.Clientes.Events
{
    public class ClienteCadastroEventHandler :
        INotificationHandler<ClienteCadastradoEvent>,
        INotificationHandler<CadastroClienteCompletoEvent>,
        INotificationHandler<ClienteEnderecoCadastradoEvent>
    {
        private readonly IClienteReadRepository _clienteReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteCadastroEventHandler(IClienteReadRepository clienteReadRepository, IUnitOfWork unitOfWork)
        {
            _clienteReadRepository = clienteReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ClienteCadastradoEvent message, CancellationToken cancellationToken)
        {
            var cliente = 
                Cliente.Factory.CriarClienteCadastradoEvento(
                    message.Id, 
                    message.Nome, 
                    message.Sobrenome, 
                    message.Email, 
                    message.Cpf);

            _clienteReadRepository.AdicionarCliente(cliente);

            await _unitOfWork.Commit();
        }

        public async Task Handle(CadastroClienteCompletoEvent message, CancellationToken cancellationToken)
        {
            var cliente = 
                Cliente.Factory.CriarCadastroClienteCompletoEvento(
                    message.ClienteId, message.Nome, message.Sobrenome,
                    message.DataNascimento, message.Cpf, message.Email,
                    message.Ativo, message.DataCadastro, message.Celular,
                    message.Profissao, message.Sexo);

            _clienteReadRepository.AtualizarCliente(cliente);

            await _unitOfWork.Commit();
        }

        public async Task Handle(ClienteEnderecoCadastradoEvent message, CancellationToken cancellationToken)
        {
            var cliente = 
                Cliente.Factory.CriarClienteEnderecoCadastradoEvento(
                    message.ClienteId, message.Nome, message.Sobrenome, 
                    message.DataNascimento, message.Cpf, message.Email,
                    message.Ativo, message.DataNascimento, message.Celular, 
                    message.Profissao, message.Sexo, message.EnderecoId,
                    message.Logradouro, message.Numero, message.Complemento,
                    message.Bairro, message.Cep, message.Cidade, message.Estado);

            _clienteReadRepository.AtualizarCliente(cliente);

            await _unitOfWork.Commit();
        }
    }
}
