using Orange_Backend.Areas.Admin.ViewModels.Review;
using Orange_Backend.Models;

namespace Orange_Backend.ViewModels
{
    public class ProductDetailPageVM
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ReviewCreateVM NewReview { get; set; }
        public  List<ReviewVM> Reviews { get; set; }
    }
}
