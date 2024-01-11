namespace Orange_Backend.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
