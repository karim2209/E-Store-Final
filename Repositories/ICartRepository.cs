namespace ElectronicsStore.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int electronicId, int quantity);
        Task<int> RemoveItem(int electronicId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout();        
    }
}
