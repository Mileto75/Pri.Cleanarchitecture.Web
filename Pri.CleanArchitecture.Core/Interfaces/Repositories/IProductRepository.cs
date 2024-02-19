using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        IQueryable<Product> GetAll();
        Task<bool> CreateAsync(Product toAdd);
        Task<bool> UpdateAsync(Product toUpdate);
        Task<bool> DeleteAsync(Product toDelete);

    }
}
