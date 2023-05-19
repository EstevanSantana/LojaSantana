using LS.Infra.Data.Read.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LS.Infra.Data.Read.Context
{
    public class ReadContext : IReadContext
    {
        private readonly ILogger<IReadContext> _logger;

        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public ReadContext(IConfiguration configuration, ILogger<IReadContext> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            try
            {
                using (Session = await MongoClient.StartSessionAsync())
                {
                    //Session.StartTransaction();

                    var commandTasks = _commands.Select(c => c());

                    await Task.WhenAll(commandTasks);
                    //await Session.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, nameof(ReadContext));
                throw;
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

            _logger.LogInformation("Iniciando mongoDb");

            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);
            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }
    }
}
