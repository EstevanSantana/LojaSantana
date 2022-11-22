using LS.Domain.Core.DomainObjects;
using System;

namespace LS.Domain.Core.Data
{
    public interface IWriteRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
