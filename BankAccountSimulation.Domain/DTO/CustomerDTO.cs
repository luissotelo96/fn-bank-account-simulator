namespace BankAccountSimulation.Domain.DTO
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Name { get; set; }
        public CustomerTypeDTO? CustomerType { get; set; }
        public string? PhoneNumber { get; set; }
        public int? LegalRepresentativeID { get; set; }
        public CustomerDTO? LegalRepresentative { get; set; }
    }
}
