using LS.Domain.Clientes.Enum;
using LS.Domain.Clientes.Models;
using LS.Domain.Core.DomainObjects;
using LS.UnitTests.Domain.Fixtures;
using Xunit;

namespace LS.UnitTests.Domain
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTest
    {
        private readonly ClienteTestFixture _clienteFixture;

        public ClienteTest(ClienteTestFixture clienteFixture) => _clienteFixture = clienteFixture;

        private Cliente ClienteValido() => _clienteFixture.GerarClienteCadastradoValido();

        #region Metodos Cadastro
        [Fact(DisplayName = "Criar novo Cliente via Fabrica de cadastro")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastro_CriarNovoCliente_Sucesso()
        {
            //Arrange & Act & Assert
            Assert.NotNull(ClienteValido());
        }

        [Fact(DisplayName = "Validar criação de novo Cliente via Fabrica de Cadastro com valores nulos")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastro_ValidarCriacaoNovoCliente_Exception()
        {
            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => _clienteFixture.GerarClienteCadastradoInValido());
        }

        [Fact(DisplayName = "Validar criação de novo Cliente via Fabrica de Cadastro com Nome informado.")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarCliente_ValidarCriacaoNovoClienteComNomeInformado_Exception()
        {
            //Arrange
            var vazio = string.Empty;
            var nome = "Estevan";

            //Act & Assert
            Assert.Throws<DomainException>(() =>    
                Cliente.Factory.CriarCadastroCliente(nome, vazio, vazio, vazio));
        }

        [Fact(DisplayName = "Validar criação de novo Cliente via Fabrica Cadastro com Sobrenome informado.")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastro_ValidarCriacaoNovoClienteComSobrenomeInformado_Exception()
        {
            //Arrange
            var vazio = string.Empty;
            var sobreNome = "Santana";

            //Act & Assert
            Assert.Throws<DomainException>(() =>
                Cliente.Factory.CriarCadastroCliente(vazio, sobreNome, vazio, vazio));
        }

        [Fact(DisplayName = "Validar criação de novo Cliente via Fabrica Cadastro com Email informado.")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastro_ValidarCriacaoNovoClienteComEmailInformado_Exception()
        {
            //Arrange
            var vazio = string.Empty;
            var email = "teste@teste.com";

            //Act & Assert
            Assert.Throws<DomainException>(() => 
                Cliente.Factory.CriarCadastroCliente(vazio, vazio, email, vazio));
        }

        [Fact(DisplayName = "Validar criação de novo Cliente via Fabrica Cadastro com CPF informado.")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastro_ValidarCriacaoNovoClienteComCpfInformado_Exception()
        {

            //Arrange
            var vazio = string.Empty;
            var cpf = "40463258010";

            //Act & Assert
            Assert.Throws<DomainException>(() => 
                Cliente.Factory.CriarCadastroCliente(vazio, vazio, vazio, cpf));
        }
        
        [Fact(DisplayName = "Validar criação de novo Evento Cliente via Fabrica Cadastro.")]
        [Trait("Categoria", "Domain - Cliente Cadastro")]
        public void FactoryCriarClienteCadastroEvento_ValidarCriacaoNovoClienteComCpfInformado_Sucesso()
        {
            //Arrange
            var clienteFaker = _clienteFixture.GerarClienteCadastradoValido();

            //Act
            var cliente = 
                Cliente.Factory.CriarClienteCadastradoEvento(
                    clienteFaker.Id, clienteFaker.Nome, clienteFaker.Sobrenome,
                    clienteFaker.Email.Endereco, clienteFaker.Cpf.Numero);

            //Assert
            Assert.NotNull(cliente.Id.ToString());
            Assert.NotNull(cliente.Nome);
            Assert.NotNull(cliente.Sobrenome);
            Assert.NotNull(cliente.Email.Endereco);
            Assert.NotNull(cliente.Cpf.Numero);
        }
        #endregion

        #region Metodos Obter Nome Completo
        [Fact(DisplayName = "Chamada da montagem do Nome Completo.")]
        [Trait("Categoria", "Domain - Cliente")]
        public void ObterNomeCompletoCliente_StringNaoNull()
        {
            //Arrange
            var cliente = ClienteValido();

            //Act & Assert
            Assert.NotNull(cliente.ObterNomeCompletoCliente());
        }

        [Fact(DisplayName = "Valida chamada da montagem do Nome Completo.")]
        [Trait("Categoria", "Domain - Cliente")]
        public void ObterNomeCompletoCliente_ValidaChamada_Sucesso()
        {
            //Arrange
            var cliente = ClienteValido();

            //Act & Assert
            Assert.Equal($"{cliente.Nome} {cliente.Sobrenome}", cliente.ObterNomeCompletoCliente());
        }
        #endregion

        #region Criar Endereco
        [Fact(DisplayName = "Criação valida do Cliente via Fabrica Completar Cadastro")]
        [Trait("Categoria", "Domain - Cliente Cadastro Endereco")]
        public void FactoryCriarClienteEndereco_CriarNovoEndereco_Sucesso()
        {
            //Arrange
            var cliente = _clienteFixture.GerarCadastroClienteCompleto();

            //Act
            cliente.AdicionarEndereco("Rua ABC", "1001", "Casa 1", "Bairro XPTO", "000000", "Teste", "TE");

            //Assert
            Assert.NotNull(cliente.Endereco);
        }
        #endregion

        #region MetodosCompletaCadastro
        [Fact(DisplayName = "Criação valida do Cliente Completar Cadastro")]
        [Trait("Categoria", "Domain - Cliente Completa Cadastro")]
        public void FactoryCriarClienteEndereco_CompletaOCliente_Sucesso()
        {
            //Arrange
            var faker = _clienteFixture.GerarCadastroClienteCompleto();

            //Act
            faker.CompletarCadastroCliente(
                faker.Id, 
                faker.DataNascimento,
                ESexo.Feminino, 
                faker.Profissao,
                faker.Celular);

            //Assert
            Assert.NotNull(faker.Sexo);
            Assert.NotNull(faker.Profissao);
            Assert.NotNull(faker.Celular);
            Assert.NotNull(faker.DataNascimento.ToString());
        }

        [Fact(DisplayName = "Deve validar a idade com erro")]
        [Trait("Categoria", "Domain - Cliente Completa Cadastro")]
        public void FactoryCriarClienteEndereco_ValidarIdadeComErro_Erro()
        {
            //Arrange
            var faker = _clienteFixture.GerarClienteCadastradoValido();

            //Act & Assert
            Assert.Throws<DomainException>(() => 
                faker.CompletarCadastroCliente(
                    faker.Id, 
                    faker.DataNascimento,
                    ESexo.Feminino,
                    faker.Profissao,
                    faker.Celular));
        }
        #endregion
    }
}
