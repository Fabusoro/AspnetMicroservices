using Catalogue.API.Data;
using Catalogue.API.Entities;
using Catalogue.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogueContext _catalogueContext;

        public ProductRepository(ICatalogueContext catalogueContext)
        {
            _catalogueContext = catalogueContext ?? throw new ArgumentNullException(nameof(catalogueContext));
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogueContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _catalogueContext.Products.DeleteOneAsync(filter);
             return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogueContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _catalogueContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _catalogueContext.Products.Find(filter).ToListAsync();      
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            
            return await _catalogueContext.Products.Find(product => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogueContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
