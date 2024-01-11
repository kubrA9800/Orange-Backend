namespace Orange_Backend.Models
{
    public class Category:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

    }
}
