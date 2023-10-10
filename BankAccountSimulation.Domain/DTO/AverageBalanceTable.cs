﻿namespace BankAccountSimulation.Domain.DTO
{
    public class AverageBalanceTable
    {
        public string? ProductType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
    }
}
