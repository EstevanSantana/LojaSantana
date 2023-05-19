using System.Linq;
using LS.Application.Clientes.Commands;
using Xunit;

namespace LS.UnitTests.Application.Clientes.Commands
{
    public class ClienteCadastroCommandTest
    {
        [Fact(DisplayName = "Cadastro Cliente valido")]
        [Trait("Categoria", "Application - Command - Cliente Cadastro")]
        public void ClienteCadastroCommand_ComandoDeveEstaValido_Sucesso()
        {
            //Arrange
            var clienteCommand = new CadastroClienteCommand("Estevan", "Santana", "40463258010", "teste@teste.com");

            //Act 
            var result = clienteCommand.EhValido();

            //Assert
            Assert.NotNull(clienteCommand.ValidationResult);
            Assert.True(result);
        }

        [Fact(DisplayName = "Cadastro Cliente Inválido")]
        [Trait("Categoria", "Application - Command - Cliente Cadastro")]
        public void ClienteCadastroCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var clienteCommand = new CadastroClienteCommand(string.Empty, string.Empty, string.Empty, string.Empty);

            // Act
            var result = clienteCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains("O Nome deve ser informado.", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O Sobrenome deve ser informado.", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O Email informado não é valido.", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O CPF informado não é valido.", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
