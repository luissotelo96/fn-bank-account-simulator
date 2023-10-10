namespace BankAccountSimulation.Domain.Entities
{
    public class FinancialProduct
    {
        public int FinancialProductID { get; set; }
        public int ProductTypeID { get; set; }
        public decimal? Balance { get; set; }
        public DateTime DateInit { get; set; }
        public bool State { get; set; }
        public int CustomerID { get; set; }
        public decimal? MontlyInterest { get; set; }
    }
}
