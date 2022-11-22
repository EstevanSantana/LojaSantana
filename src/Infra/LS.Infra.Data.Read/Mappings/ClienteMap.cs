using LS.Domain.Clientes.Models;
using MongoDB.Bson.Serialization;

namespace LS.Infra.Data.Read.Mappings
{
    public class ClienteMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Cliente>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
