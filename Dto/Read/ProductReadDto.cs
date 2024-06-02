﻿namespace MyApp.Dto.Read
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        public IEnumerable<CategoryReadDto> Categories { get; set; }
    }
}