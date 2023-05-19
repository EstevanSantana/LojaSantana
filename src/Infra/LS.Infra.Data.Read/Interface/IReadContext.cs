using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace LS.Infra.Data.Read.Interface
{
    public interface IReadContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
