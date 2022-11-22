using LS.Domain.Clientes.Models;
using LS.Domain.Core.DomainObjects;
using System;
using Xunit;

namespace LS.UnitTests.Domain
{
    public class EnderecoTest
    {
        [Fact(DisplayName = "Criação valida do Endereco via Fabrica Checkout")]
        [Trait("Categoria", "Domain - Cliente Endereco Checkout")]
        public void FactoryCriarEnderecoCheckout_CriarNovoEndereco_Sucesso()
        {
            //Arrange
            var clienteId = Guid.NewGuid();
            var endereco = 
                Endereco.Factory.CriarEnderecoCompletarCadastroCliente(
                    "Rua ABC", "1001", "Casa 1", "Bairro XPTO", "000000", "Teste", "TE", clienteId);

            //Act & Assert
            Assert.NotNull(endereco);
        }

        [Fact(DisplayName = "Validar do Endereco via Fabrica Checkout")]
        [Trait("Categoria", "Domain - Cliente Endereco Checkout")]
        public void FactoryCriarEnderecoCheckout_ValidarACriacao_Exception()
        {
            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => 
                Endereco.Factory.CriarEnderecoCompletarCadastroCliente("", "", "", "", "", "", "", Guid.Empty));
        }

    }
}
