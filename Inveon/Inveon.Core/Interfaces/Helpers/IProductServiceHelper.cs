namespace Inveon.Core.Interfaces.Helpers
{
    public interface IProductServiceHelper
    {
        bool IsThereStock(string productIdx, int quantity);
    }
}
