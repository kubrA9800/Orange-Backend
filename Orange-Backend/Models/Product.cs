﻿namespace Orange_Backend.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
        public BrandCategory BrandCategory { get; set; }
        public int? BrandCategoryId { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        //public int BrandId { get; set; }
        //public Brand Brand { get; set; }
    }
}
