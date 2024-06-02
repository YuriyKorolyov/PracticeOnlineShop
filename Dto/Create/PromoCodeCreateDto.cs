namespace MyApp.Dto.Create
{
    public class PromoCodeCreateDto
    {
        public string PromoName { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
