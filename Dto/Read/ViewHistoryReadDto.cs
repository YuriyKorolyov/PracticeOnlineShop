﻿namespace MyApp.Dto.Read
{
    public class ViewHistoryReadDto
    {
        public int Id { get; set; }
        public DateTime ViewDate { get; set; }

        public ProductReadDto Product { get; set; }
    }
}