﻿using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        IQueryable<Property> GetAll();
        Task<bool> CreateAsync(Property toAdd);
        Task<bool> UpdateAsync(Property toUpdate);
        Task<bool> DeleteAsync(Property toDelete);
    }
}
