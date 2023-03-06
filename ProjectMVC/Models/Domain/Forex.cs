namespace ProjectMVC.Models.Domain
{
    public class Forex
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public int Unit { get; set; }
        public string CurrencyName { get; set; }
        public double ForexBuying { get; set; }
        public double ForexSelling { get; set; }
        public double BanknoteBuying { get; set; }
        public double BanknoteSelling { get; set; }
        public int Date { get; set; }
        
    }
}
