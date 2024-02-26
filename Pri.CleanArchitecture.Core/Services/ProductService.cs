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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IPropertyRepository propertyRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<ProductResultModel> CreateAsync(string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            //check if product name exists
            if (await _productRepository
                .GetAll()
                .AnyAsync(p => p.Name.ToUpper() == name.ToUpper()))
            {
                return new ProductResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Name exists!" }
                };
            }
            //check if categoryId exists
            if (!await _categoryRepository.GetAll().AnyAsync(c => c.Id == categoryId))
            {
                return new ProductResultModel
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Unknown category!" }
                };
            }
            //check if propertyIds exist
            foreach (var propertyId in propertyIds)
            {
                if (await _propertyRepository.GetByIdAsync(propertyId) == null)
                {
                    return new ProductResultModel
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "PropertyId does not exist!" }
                    };
                }
            }
            //create the entity
            //call the repo method
            Product newProduct = new Product
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                Properties = await _propertyRepository
                .GetAll()
                .Where(p => propertyIds.Contains(p.Id)).ToListAsync(),
                Description = description,
            };
            if (await _productRepository.CreateAsync(newProduct))
            {
                return new ProductResultModel
                {
                    IsSuccess = true,
                    Products = new List<Product> { newProduct }
                };
            }
            return new ProductResultModel
            {
                IsSuccess = false,
                Errors = new List<string> { "Product not created!" }
            };
        }

        public Task<ProductResultModel> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResultModel> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
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

        public async Task<ProductResultModel> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
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

        public Task<ProductResultModel> UpdateAsync(int id, string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            throw new NotImplementedException();
        }
    }
}
