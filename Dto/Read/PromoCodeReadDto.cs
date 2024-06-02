namespace MyApp.Dto.Read
{
    public class PromoCodeReadDto
    {
        public int Id { get; set; }
        public string PromoName { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
