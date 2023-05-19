using Bogus;
using LS.Application.Clientes.Dtos;
using LS.Domain.Clientes.Enum;
using LS.IntegrationTests.Configurations;
using LS.IntegrationTests.Models;
using LS.Services.Api;
using LS.UnitTests.Domain.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LS.IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ClienteScenarioTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        private readonly ClienteTestFixture _clienteTestFixture;

        private readonly Guid CLIENTE_ID = Guid.Parse("16ca2719-9871-4a20-90d5-d5a54cb555a0");

        public ClienteScenarioTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
            _clienteTestFixture = new ClienteTestFixture();
        }

        [Fact(DisplayName = "Completar Cadastro com sucesso")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_CompletarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();
            var cliente = new CompletaCadastroClienteDto()
            {
                ClienteId = CLIENTE_ID,
                DataNascimento = faker.DataNascimento,
                Celular = faker.Celular,
                Profissao = faker.Profissao,
                Sexo = ESexo.Masculino
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PutAsync("api/v1/cliente/completa-cadastro/", _testsFixture.MontarContent(cliente));

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Completar Cadastro id não valido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_CompletarCadastroIdNaoValido_DeveExecutarComErro()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarCadastroClienteCompleto();
            var cliente = new CompletaCadastroClienteDto()
            {
                ClienteId = faker.Id,
                DataNascimento = faker.DataNascimento,
                Celular = faker.Celular,
                Profissao = faker.Profissao,
                Sexo = ESexo.Masculino
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var response = await _testsFixture.Client.PutAsync("api/v1/cliente/completa-cadastro/", _testsFixture.MontarContent(cliente));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.Status);
            Assert.Contains("O Id informado não existe.", error.Errors.Messages);
        }
    
        [Fact(DisplayName = "Completar Cadastro cliente não valido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_CompletarCadastroClienteNaoValido_DeveExecutarComErro()
        {
            // Arrange
            var cliente = new CompletaCadastroClienteDto()
            {
                ClienteId = Guid.Empty,
                DataNascimento = DateTime.MinValue,
                Celular = string.Empty,
                Profissao = string.Empty,
                Sexo = 0
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var response = await _testsFixture.Client.PutAsync("api/v1/cliente/completa-cadastro/", _testsFixture.MontarContent(cliente));
            var message = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, message.Status);
            Assert.NotNull(message.Errors.Messages);
        }

        [Fact(DisplayName = "Adicionar Endereco com sucesso")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_AdicionarEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = new Faker("pt_BR");

            var endereco = new AdicionarClienteEnderecoDto()
            {
                ClienteId = CLIENTE_ID,
                Logradouro = faker.Address.StreetName(),
                Numero = faker.Address.BuildingNumber(),
                Complemento = "casa",
                Bairro = faker.Address.City(),
                Cep = faker.Address.ZipCode(),
                Cidade = faker.Address.City(),
                Estado = faker.Address.State()
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsync("api/v1/cliente/adicionar-endereco/", _testsFixture.MontarContent(endereco));

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
