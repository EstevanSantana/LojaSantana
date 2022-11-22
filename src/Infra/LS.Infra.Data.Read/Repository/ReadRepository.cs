using LS.Domain.Core.Data;
using LS.Domain.Core.DomainObjects;
using LS.Infra.Data.Read.Interfaces;
using MongoDB.Driver;

namespace LS.Infra.Data.Read.Repository
{
    public abstract class ReadRepository<T> : IReadRepository<T> where T : IAggregateRoot
    {
        protected readonly IReadContext Context;
        protected IMongoCollection<T> DbSet;

        public ReadRepository(IReadContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }

        public void Dispose() => Context?.Dispose();
    }
}
