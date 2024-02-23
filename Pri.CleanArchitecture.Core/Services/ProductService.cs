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
        public Task<ProductResultModel> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResultModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
