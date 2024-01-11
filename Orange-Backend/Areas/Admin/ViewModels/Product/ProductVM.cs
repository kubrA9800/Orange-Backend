﻿using Orange_Backend.Models;

namespace Orange_Backend.Areas.Admin.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
