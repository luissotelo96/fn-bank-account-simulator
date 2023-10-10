namespace BankAccountSimulation.Domain.DTO
{
    public class FinancialProductDTO
    {
        public int FinancialProductID { get; set; }
        public int ProductTypeID { get; set; }
        public decimal? Balance { get; set; }
        public DateTime DateInit { get; set; }        
        public int CustomerID { get; set; }
        public ProductTypeDTO? ProductType { get; set; }
        public decimal? MontlyInterest { get; set; }
        public List<FinancialMovementDTO>? FinancialMovements { get; set; }
    }
}
