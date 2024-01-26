using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Helpers.Responses;
using Orange_Backend.Models;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.Services.Interfaces
{
    public interface ICartService
    {
        void AddToBasket(int id, Product product);
        List<CartVM> GetDatasFromCookies();
        void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct);
        int GetCount();
        Task<List<CartDetailVM>> GetBasketDatasAsync();
        Task<DeleteBasketItemResponse> DeleteItem(int id);
        Task<CountResponse> IncreaseProductCount(int id);
        Task<CountResponse> DecreaseProductCount(int id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByBasketIdAsync(int? basketId);
    }
}
