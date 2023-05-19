using LS.Application.Clientes.Dtos;
using LS.IntegrationTests.Configurations;
using LS.UnitTests.Domain.Fixtures;
using System.Threading.Tasks;
using LS.Services.Api;
using LS.Services.Api.Dtos;
using Xunit;

namespace LS.IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UsuarioScenarioTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        private readonly ClienteTestFixture _clienteTestFixture;

        public UsuarioScenarioTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
            _clienteTestFixture = new ClienteTestFixture();
        }

        [Fact(DisplayName = "Realizar cadastro com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração - Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteCadastradoValido();

            _testsFixture.GerarUserSenha();

            var usuario = new CadastroClienteDto()
            {
                Nome = faker.Nome,
                Sobrenome = faker.Sobrenome,
                Cpf = faker.Cpf.Numero,
                Email = _testsFixture.UsuarioEmail,
                Senha = _testsFixture.UsuarioSenha,
                ConfirmaSenha = _testsFixture.UsuarioSenha
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsync("api/v1/identidade/nova-conta/", _testsFixture.MontarContent(usuario));

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        
        [Fact(DisplayName = "Realizar login com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração - Usuário")]
        public async Task Usuario_RealizarLogin_DeveExecutarComSucesso()
        {
            // Arrange
            var usuario = new UsuarioLoginDto()
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsync("api/v1/identidade/entrar/", _testsFixture.MontarContent(usuario));

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
