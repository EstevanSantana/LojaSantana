using LS.Domain.Core.Data;
using LS.Domain.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LS.Domain.Clientes.Interfaces
{
    public interface IClienteReadRepository : IReadRepository<Cliente> 
    {
        Task<Cliente> ObterPorId(Guid id);
        Task<IEnumerable<Cliente>> ObterTodos();
        
        void AdicionarCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
    }
}
