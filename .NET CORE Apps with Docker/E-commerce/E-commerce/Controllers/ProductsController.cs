using E_commerce.ApiModels;
using E_commerce.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IProductLogic _productLogic;

        public ProductsController(ILogger<ProductsController> logger, IProductLogic productLogic)
        {
            this.logger = logger;
            _productLogic = productLogic;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(string category = "all")
        {
            logger.LogInformation("Starting controller action GetProducts for {category}", category);

            return _productLogic.GetProductsForCategory(category);
        }
    }
}
