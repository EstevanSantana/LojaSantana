using LS.Application.Clientes.Dtos;
using System;
using System.Threading.Tasks;

namespace LS.Application.Clientes.Queries
{
    public interface IClienteQuerie 
    {
        Task<ClienteDto> ObterPorId(Guid id);
    }
}
