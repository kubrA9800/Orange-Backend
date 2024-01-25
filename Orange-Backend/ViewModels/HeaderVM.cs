using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.ViewModels
{
    public class HeaderVM
    {
        public string Logo { get; set; }
        public string UserFullName { get; set; }
        public int BasketCount { get; set; }
        public int WishlistCount { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<CartDetailVM> SidebarCartProducts { get; set; }
    }
}
