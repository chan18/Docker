using E_commerce.ApiModels;
using E_commerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(ILogger<ProductsController> logger, IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(string category = "all")
        {
            //Log.Information("Starting controller action GetProducts for {category}", category);
            Log.ForContext("Category", category)
              .Information("Starting controller action GetProducts");

            return _productLogic.GetProductsForCategory(category);
        }
    }
}
