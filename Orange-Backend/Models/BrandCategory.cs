namespace Orange_Backend.Models
{
    public class BrandCategory:BaseEntity
    {
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
