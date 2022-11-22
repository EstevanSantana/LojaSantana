using LS.Domain.Core.Data;
using LS.Domain.Clientes.Models;
using System;
using System.Threading.Tasks;

namespace LS.Domain.Clientes.Interfaces
{
    public interface IClienteWriteRepository : IWriteRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        void ApagarCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void AdicionarEndereco(Endereco endereco);

        Task<Cliente> ObterPorCpf(string cpf);
        Task<Cliente> ObterPorId(Guid id);
    }
}
