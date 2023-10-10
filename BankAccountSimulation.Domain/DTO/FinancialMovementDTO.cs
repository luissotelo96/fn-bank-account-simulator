namespace BankAccountSimulation.Domain.DTO
{
    public class FinancialMovementDTO
    {
        public int FinancialMovementsID { get; set; }
        public int FinancialProductID { get; set; }
        public DateTime MovementDate { get; set; }
        public string? MovementType { get; set; }
        public decimal? Value { get; set; }
        public int CustomerID { get; set; }
        public decimal? Balance { get; set; }
    }


}
