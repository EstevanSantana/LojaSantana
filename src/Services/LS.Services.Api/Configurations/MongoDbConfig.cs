using LS.Infra.Data.Read.Mappings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace LS.Services.Api.Configurations
{
    public static class MongoDbConfig
    {
        public static void Configure()
        {
            ClienteMap.Configure();

            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };

            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}
