using Orange_Backend.Models;
using Orange_Backend.ViewModels.Wishlist;

namespace Orange_Backend.Services.Interfaces
{
    public interface IWishlistService
    {
        int AddToWishlist(int id, Product product);
        int GetCount();
        Task<List<WishlistDetailVM>> GetWishlistDatasAsync();
        void DeleteItem(int id);
        List<WishlistVM> GetDatasFromCookies();
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? basketId);

        //void SetDatasToCookies(List<WishlistVM> wishlist, Product dbProduct, WishlistVM existProduct);

    }
}
