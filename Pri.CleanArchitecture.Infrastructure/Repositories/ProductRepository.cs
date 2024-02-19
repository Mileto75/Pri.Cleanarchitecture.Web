using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext applicationDbContext, ILogger<ProductRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Product toAdd)
        {
            _applicationDbContext.Products.Add(toAdd);
            try
            {
               await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogCritical(dbUpdateException.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Product toDelete)
        {
            _applicationDbContext.Remove(toDelete);
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogCritical(dbUpdateException.Message);
                return false;
            }
        }

        public IQueryable<Product> GetAll()
        {
            return _applicationDbContext.Products.AsQueryable();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _applicationDbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _applicationDbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _applicationDbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(Product toUpdate)
        {
            _applicationDbContext.Products.Update(toUpdate);
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogCritical(dbUpdateException.Message);
                return false;
            }
        }
    }
}
