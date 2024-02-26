using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResultModel> GetAllAsync(); 
        Task<ProductResultModel> GetByIdAsync(int id);
        Task<ProductResultModel> 
            CreateAsync(string name,int categoryId,string description,decimal price, IEnumerable<int> propertyIds);
        Task<ProductResultModel> UpdateAsync(int id,string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds);
        Task<ProductResultModel> DeleteAsync(int id);
        Task<ProductResultModel> SearchByNameAsync(string name);
        Task<ProductResultModel> SearchByCategoryIdAsync(int categoryId);
    }
}
