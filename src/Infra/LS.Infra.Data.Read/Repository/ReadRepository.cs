using LS.Domain.Core.Data;
using LS.Domain.Core.DomainObjects;
using LS.Infra.Data.Read.Interface;
using MongoDB.Driver;

namespace LS.Infra.Data.Read.Repository
{
    public abstract class ReadRepository<T> : IReadRepository<T> where T : IAggregateRoot
    {
        protected readonly IReadContext Context;
        protected IMongoCollection<T> DbSet;

        protected ReadRepository(IReadContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }

        public void Dispose() => Context?.Dispose();
    }
}
