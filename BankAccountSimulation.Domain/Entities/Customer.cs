namespace BankAccountSimulation.Domain.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Name { get; set; }
        public bool State { get; set; }
        public int? CustomerTypeID { get; set; }
        public string? PhoneNumber { get; set; }
        public int? LegalRepresentativeID { get; set; }
    }
}
