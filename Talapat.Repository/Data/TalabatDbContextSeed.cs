using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talapat.Repository.Data
{
    public static class TalabatDbContextSeed
    { 
        public static async Task SeedAsync(TalabatDbContext _dbContext)
        {
            if (_dbContext.productBrands.Count()==0 )
            {
                var productBrands = File.ReadAllText("../Talapat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.productBrands.Add(brand);
                    }
                    _dbContext.SaveChanges();
                }
            }
            if (_dbContext.ProductCategories.Count()==0 )
            {
                var productCategories = File.ReadAllText("../Talapat.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(productCategories);
                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbContext.ProductCategories.Add(category);
                    }
                    _dbContext.SaveChanges();
                }
            }
            if(_dbContext.Products.Count()==0)
            {
                var jsonProducts = File.ReadAllText("../Talapat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);
                if (products?.Count > 0)
                {
                    foreach(var product in products)
                    {
                        _dbContext.Products.Add(product);
                    }
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
