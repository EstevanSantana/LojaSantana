using LS.Application.Clientes.Commands;
using LS.Application.Clientes.Events;
using LS.Domain.Clientes.Enum;
using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using LS.UnitTests.Domain.Fixtures;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LS.UnitTests.Application.Clientes.Commands
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteCommandHandlerTest
    {
        private readonly Cliente _cliente;
        private readonly AutoMocker _mocker;
        private readonly ClienteCommandHandler _clienteCommandHandler;
        private readonly ClienteTestFixture _clienteTestFixture;

        public ClienteCommandHandlerTest(ClienteTestFixture clienteTestFixture)
        {
            _mocker = new AutoMocker();
            _clienteTestFixture = clienteTestFixture;
            _clienteCommandHandler = _mocker.CreateInstance<ClienteCommandHandler>();
        }

        #region ClienteCadastro
        [Fact(DisplayName = "Cliente deve ser Cadastrado com sucesso")]
        [Trait("Categoria", "Application - Command Handler - Cliente Cadastro")]
        public async Task ClienteCadastro_NovoCliente_Sucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteCadastradoValido();
            var command = new CadastroClienteCommand(faker.Nome, faker.Sobrenome, faker.Cpf.Numero, faker.Email.Endereco);

            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _clienteCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(command.EhValido());
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.Adicionar(It.IsAny<Cliente>()), Times.Once);
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        
        [Fact(DisplayName = "Evento Cliente deve ser Adicionado")]
        [Trait("Categoria", "Application - Command Handler - Cliente Cadastro")]
        public void EventoClienteCadastro_NovoEventoCliente_Sucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteCadastradoValido();
            var command = new CadastroClienteCommand(faker.Nome, faker.Sobrenome, faker.Cpf.Numero, faker.Email.Endereco);

            // Act
            faker.AdicionarEvento(new ClienteCadastradoEvent(faker.Id, command.Nome, command.Sobrenome, command.Cpf, command.Email, faker.DataCadastro));
            
            // Assert
            Assert.NotNull(faker.Notificacoes);
        }
        #endregion

        [Fact(DisplayName = "Evento Completa Cadastro Cliente deve ser Adicionado")]
        [Trait("Categoria", "Application - Command Handler - Cliente Completa Cadastro")]
        public void EventoClienteCompletaCadastro_NovoEventoCliente_Sucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();

            // Act
            faker.AdicionarEvento(new CadastroClienteCompletoEvent(
                faker.Id, faker.Nome, faker.Sobrenome, faker.DataNascimento, faker.Cpf.Numero, faker.Email.Endereco,
                    faker.Ativo, faker.DataCadastro, faker.Celular, faker.Profissao, faker.Profissao));

            // Assert
            Assert.NotNull(faker.Notificacoes);
        }

        [Fact(DisplayName = "Cliente deve ser Completada Cadastro com sucesso")]
        [Trait("Categoria", "Application - Command Handler - Cliente Completa Cadastro")]
        public async Task ClienteCompletaCadastro_AtualizarCliente_Sucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();
            var command = new CompletaCadastroClienteCommand(faker.Id, faker.DataNascimento, faker.Celular, faker.Profissao, ESexo.Masculino);
            
            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.ObterPorId(command.ClienteId)).Returns(Task.FromResult(faker));
            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _clienteCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(command.EhValido());
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.AtualizarCliente(It.IsAny<Cliente>()), Times.Once);
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Validar Completada Cadastro com ClienteId Invalido")]
        [Trait("Categoria", "Application - Command Handler - Cliente Completa Cadastro")]
        public async Task ClienteCompletaCadastro_ValidarComando_ValidationResult()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();
            var command = new CompletaCadastroClienteCommand(faker.Id, faker.DataNascimento, faker.Celular, faker.Profissao, ESexo.Masculino);
            
            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.ObterPorId(command.ClienteId)).Returns(Task.FromResult(_cliente));
            
            // Act
            var result = await _clienteCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Contains("O Id informado não existe.", result.Errors.Select(c => c.ErrorMessage));
        }


        [Fact(DisplayName = "Completada Cadastro com erro no Commit")]
        [Trait("Categoria", "Application - Command Handler - Cliente Completa Cadastro")]
        public async Task ClienteCompletaCadastro_DeveExceutaOCommitErrado_ValidationResult()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();
            var command = new CompletaCadastroClienteCommand(faker.Id, faker.DataNascimento, faker.Celular, faker.Profissao, ESexo.Masculino);

            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.ObterPorId(command.ClienteId)).Returns(Task.FromResult(faker));
            _mocker.GetMock<IClienteWriteRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(false));

            // Act
            var result = await _clienteCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Contains("Erro ao tentar salvar no banco.", result.Errors.Select(c => c.ErrorMessage));
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.AtualizarCliente(It.IsAny<Cliente>()), Times.Once);
            _mocker.GetMock<IClienteWriteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}
