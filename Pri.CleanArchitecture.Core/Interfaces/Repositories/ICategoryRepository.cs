using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync();
        IQueryable<Category> GetAll();
        Task<bool> CreateAsync(Category toAdd);
        Task<bool> UpdateAsync(Category toUpdate);
        Task<bool> DeleteAsync(Category toDelete);
    }
}
