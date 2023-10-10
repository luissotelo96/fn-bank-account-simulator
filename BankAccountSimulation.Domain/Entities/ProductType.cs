using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSimulation.Domain.Entities
{
    public class ProductType
    {
        public int ProductTypeID { get; set; }
        public string? Description { get; set; }
        public decimal? MontlyInterest { get; set; }
        public byte State { get; set; }
    }
}
