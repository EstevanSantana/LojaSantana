using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace LS.Infra.Data.Read.Interfaces
{
    public interface IReadContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
