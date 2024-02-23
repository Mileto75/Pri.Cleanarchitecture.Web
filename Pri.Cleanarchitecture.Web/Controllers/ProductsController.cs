using Microsoft.AspNetCore.Mvc;
using Pri.Cleanarchitecture.Web.ViewModels;
using Pri.CleanArchitecture.Core.Interfaces.Services;

namespace Pri.Cleanarchitecture.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();
            if(result.IsSuccess)
            {
                ProductsIndexViewModel productsIndexViewModel = new ProductsIndexViewModel();
                productsIndexViewModel.Products
                    = result.Products.Select(p =>
                    new BaseViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                    });
                return View(productsIndexViewModel);
            }
            return View("Error",result.Errors);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if(result.IsSuccess)
            {
                ProductsDetailviewModel productsDetailViewModel 
                    = new ProductsDetailviewModel
                    {
                        Id = result.Products.First().Id,
                        Name = result.Products.First().Name,
                        Price = result.Products.First().Price,
                        Properties = result.Products.First().Properties
                        .Select(p => p.Name),
                        Category = result.Products.First().Category.Name,
                    };
                return View(productsDetailViewModel);
            }
            return View("Error",result.Errors);
        }
    }
}
