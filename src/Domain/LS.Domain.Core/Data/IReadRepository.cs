using LS.Domain.Core.DomainObjects;
using System;

namespace LS.Domain.Core.Data
{
    public interface IReadRepository<T> : IDisposable where T : IAggregateRoot { }
}
