using LS.Infra.Data.Read.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Infra.Data.Read.Context
{
    public class ReadContext : IReadContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public ReadContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);
                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null) return;

            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);
            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }
    }
}
