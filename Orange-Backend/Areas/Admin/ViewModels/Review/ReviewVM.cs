namespace Orange_Backend.Areas.Admin.ViewModels.Review
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
