namespace Orange_Backend.Areas.Admin.ViewModels.Review
{
    public class ReviewCreateVM
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
