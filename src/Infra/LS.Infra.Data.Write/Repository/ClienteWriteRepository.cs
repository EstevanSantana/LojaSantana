using Infra.Data.Write.Context;
using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using LS.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infra.Data.Write.Repository
{
    public class ClienteWriteRepository : IClienteWriteRepository
    {
        private readonly WriteContext _context;

        public ClienteWriteRepository(WriteContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterPorCpf(string cpf)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void ApagarCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
