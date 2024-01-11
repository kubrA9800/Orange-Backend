namespace Orange_Backend.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        public string Head { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
