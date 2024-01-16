using Orange_Backend.Areas.Admin.ViewModels.Subscribe;

namespace Orange_Backend.ViewModels
{
    public class FooterVM
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public SubscribeCreateVM SubscribeEmail { get; set; }
    }
}
