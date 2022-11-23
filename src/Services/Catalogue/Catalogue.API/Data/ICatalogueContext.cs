using Catalogue.API.Entities;
using MongoDB.Driver;

namespace Catalogue.API.Data
{
    public interface ICatalogueContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
