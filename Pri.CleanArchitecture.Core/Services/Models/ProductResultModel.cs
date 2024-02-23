using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Services.Models
{
    public class ProductResultModel : BaseResultModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
