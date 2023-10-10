namespace BankAccountSimulation.Domain.Entities
{
    public class CustomerType
    {
        public int CustomerTypeID { get; set; }
        public string? Description { get; set; }
        public byte State { get; set; }
    }
}
