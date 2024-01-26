using Orange_Backend.Areas.Admin.ViewModels.Product;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.ViewModels;
using Orange_Backend.ViewModels.Cart;

namespace Orange_Backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingService _settingService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        public LayoutService(ISettingService settingService,
                             ICartService cartService,
                             IProductService productService,
                             IWishlistService wishlistService)
        {
            _settingService = settingService;
            _cartService = cartService;
            _productService = productService;
            _wishlistService = wishlistService;
        }

        public async Task<HeaderVM> GetHeaderDatas()
        {

            Dictionary<string, string> settingDatas = _settingService.GetSettings();
            List<CartDetailVM> basketProducts = await   _cartService.GetBasketDatasAsync();
            List<ProductVM> recommendedProducts = await _productService.GetAllAsync();
            int basketCount = _cartService.GetCount();
            int wishlistCount= _wishlistService.GetCount();
            return new HeaderVM
            {
                BasketCount = basketCount,
                Logo = settingDatas["Logo"],
                SidebarCartProducts = basketProducts,
                Products= recommendedProducts,
                WishlistCount= wishlistCount

            };
        }
        public FooterVM GetFooterDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            return new FooterVM
            {
                Phone = settingDatas["Phone"],
                Email = settingDatas["Email"],
            };
        }
    }
}
