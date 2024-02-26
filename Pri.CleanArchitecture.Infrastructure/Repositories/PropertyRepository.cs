using Microsoft.EntityFrameworkCore;
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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PropertyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public Task<bool> CreateAsync(Property toAdd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Property toDelete)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Property> GetAll()
        {
            return _applicationDbContext.Properties.AsQueryable();
        }

        public Task<IEnumerable<Property>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Properties
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<bool> UpdateAsync(Property toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
