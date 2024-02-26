using Microsoft.EntityFrameworkCore;
using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Core.Interfaces.Services;
using Pri.CleanArchitecture.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResultModel> CreateAsync(string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            //check if product name exists
            if(await _productRepository
                .GetAll()
                .AnyAsync(p => p.Name.ToUpper() == name.ToUpper()))
            {
                return new ProductResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> {"Name exists!"}
                };
            }
            //check if categoryId exists
            //check if propertyIds exist
        }

        public Task<ProductResultModel> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResultModel> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if(products.Count() > 0)
            {
                return new ProductResultModel
                {
                    IsSuccess = true,
                    Products = products
                };
            }
            return new ProductResultModel
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public async Task<ProductResultModel> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
            {
                return new ProductResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Product not found!" }
                };
            }
            return new ProductResultModel
            {
                IsSuccess = true,
                Products = new List<Product> { product }
            };
        }

        public async Task<ProductResultModel> SearchByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            if (products.Count() > 0)
            {
                return new ProductResultModel
                {
                    IsSuccess = true,
                    Products = products
                };
            }
            return new ProductResultModel
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public async Task<ProductResultModel> SearchByNameAsync(string name)
        {
            var products = await _productRepository.GetAll()
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .Where(p => p.Name.ToUpper().Contains(name.ToUpper()))
                .ToListAsync();
            if(products.Count() > 0)
            {
                return new ProductResultModel
                {
                    IsSuccess = true,
                    Products = products
                };
            }
            return new ProductResultModel
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public Task<ProductResultModel> UpdateAsync(int id, string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            throw new NotImplementedException();
        }
    }
}
