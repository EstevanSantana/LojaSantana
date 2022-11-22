using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using LS.Domain.Clientes.Enum;
using LS.Domain.Clientes.Models;
using System;
using Xunit;

namespace LS.UnitTests.Domain.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestFixture> { }

    public class ClienteTestFixture
    {
        public string CPF_VALIDO = "404.632.580-10";
        public string CPF_INVALIDO = "00000000000";
        public string DATA_NASCIMENTO_VALIDA = "17/11/1999";
        public string DATA_NASCIMENTO_INVALIDA = "17/11/2005";
        
        private readonly Faker<Cliente> Faker;
        
        public ClienteTestFixture()
        {
            Faker = new Faker<Cliente>("pt_BR");
        }

        public Cliente GerarClienteCadastradoInValido()
            => Cliente.Factory.CriarCadastroCliente(string.Empty, string.Empty, string.Empty, CPF_INVALIDO);

        public Cliente GerarClienteCadastradoValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = Faker.CustomInstantiator(f => 
                Cliente.Factory.CriarCadastroCliente(
                    f.Name.FirstName(genero),
                        f.Name.LastName(genero),
                            f.Internet.Email(),
                                f.Person.Cpf()));

            return cliente;
        }

        public Cliente GerarCadastroClienteCompleto() 
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var sexo = string.Empty;
            var ativo = true;

            if (genero == Name.Gender.Male) sexo = ESexo.Masculino.ToString();
            else sexo = ESexo.Feminino.ToString();

            var cliente = Faker.CustomInstantiator(f =>
                Cliente.Factory.CriarCadastroClienteCompletoEvento(
                    f.Random.Guid(),
                        f.Name.FirstName(genero),
                            f.Name.LastName(genero),
                                f.Person.DateOfBirth.AddYears(-18),
                                    f.Person.Cpf(),
                                        f.Person.Email,
                                            ativo,
                                                DateTime.Now,
                                                    f.Person.Phone,     
                                                        "TI",
                                                            sexo));

            return cliente;
        }
    }
}
