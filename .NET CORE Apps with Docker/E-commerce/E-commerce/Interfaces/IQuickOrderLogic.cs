using E_commerce.ApiModels;

namespace E_commerce.Interfaces
{
    public interface IQuickOrderLogic
    {
        Guid PlaceQuickOrder(QuickOrder order, int customerId);
    }
}
