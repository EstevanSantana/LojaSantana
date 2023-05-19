using LS.Domain.Clientes.Interfaces;
using LS.Domain.Clientes.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LS.Infra.Data.Read.Interface;

namespace LS.Infra.Data.Read.Repository
{
    public class ClienteReadRepository : ReadRepository<Cliente>, IClienteReadRepository
    {
        public ClienteReadRepository(IReadContext context) : base(context) { }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<Cliente>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            var all = await DbSet.FindAsync(Builders<Cliente>.Filter.Empty);
            return all.ToList();
        }

        public void AdicionarCliente(Cliente cliente)
        {
            Context.AddCommand(() => DbSet.InsertOneAsync(cliente));
        }

        public void AtualizarCliente(Cliente cliente)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(filter: x => x.Id == cliente.Id, replacement: cliente)); ;
        }
    }
}
