using LS.Application.Clientes.Dtos;
using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using System;
using System.Threading.Tasks;

namespace LS.Application.Clientes.Queries
{
    public class ClienteQuerie : IClienteQuerie
    {
        private readonly IClienteReadRepository _clienteReadRepository;

        public ClienteQuerie(IClienteReadRepository clienteReadRepository)
        {
            _clienteReadRepository = clienteReadRepository;
        }

        public async Task<ClienteDto> ObterPorId(Guid id)
        {
            var cliente = await _clienteReadRepository.ObterPorId(id);
            if (cliente == null) return new ClienteDto();

            return new ClienteDto()
            {
                ClienteId = cliente.Id,
                Nome = cliente.ObterNomeCompletoCliente(),
                DataNascimento = cliente.DataNascimento,
                Cpf = cliente.Cpf.Numero,
                Email = cliente.Email.Endereco,
                Ativo = cliente.MontarAtivo(),
                DataCadastro = cliente.DataCadastro,
                Celular = cliente.Celular,
                Profissao = cliente.Profissao,
                Sexo = cliente.Sexo,
            };
        }
    }
}
