using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.ViewModels
{
    public class CartPageVM
    {
        public List<ProductVM> Products { get; set; }
        public List<CartDetailVM> CartDetails { get; set; }
        
    }
}
