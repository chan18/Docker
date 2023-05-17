using E_commerce.ApiModels;

namespace E_commerce.Interfaces
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetProductsForCategory(string category);
    }
}
