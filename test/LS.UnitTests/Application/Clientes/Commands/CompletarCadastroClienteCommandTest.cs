using System;
using System.Linq;
using LS.Application.Clientes.Commands;
using LS.Domain.Clientes.Enum;
using Xunit;

namespace LS.UnitTests.Application.Clientes.Commands
{
    public class CompletarCadastroClienteCommandTest
    {
        [Fact(DisplayName = "Completa Cadastro Cliente valido")]
        [Trait("Categoria", "Application - Command - Cliente Completa Cadastro")]
        public void CompletarCadastroClienteCommand_CommandoDeveEstaValido_Sucesso()
        {
            //Arrange
            var clienteCommand = new CompletaCadastroClienteCommand(Guid.NewGuid(), Convert.ToDateTime("09/12/1995"), "11900000000", "TI", ESexo.Masculino);

            //Act 
            var result = clienteCommand.EhValido();

            //Assert
            Assert.NotNull(clienteCommand.ValidationResult);
            Assert.True(result);
        }

        [Fact(DisplayName = "Completa Cadastro Cliente Inválido")]
        [Trait("Categoria", "Application - Command - Cliente Completa Cadastro")]
        public void CompletarCadastroClienteCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var clienteCommand = new CompletaCadastroClienteCommand(Guid.Empty, DateTime.MinValue, string.Empty, string.Empty, ESexo.NaoInformado);

            // Act
            var result = clienteCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains("Informe o Id do Cliente", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Informe a Data de Nascimento", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Informe o Celular", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Informe o Sexo", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Informe a Profissao", clienteCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
