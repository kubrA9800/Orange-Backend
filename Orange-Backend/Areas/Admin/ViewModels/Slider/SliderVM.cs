namespace Orange_Backend.Areas.Admin.ViewModels.Slider
{
    public class SliderVM
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
