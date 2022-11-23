using Catalogue.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalogue.API.Data
{
    public class CatalogueContext : ICatalogueContext
    {
        public CatalogueContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogueContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products {get;}
    }
}
