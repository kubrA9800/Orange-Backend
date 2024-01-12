namespace Orange_Backend.Models
{
    public class Brand:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public List<Category> Category { get; set; }
    }
}
