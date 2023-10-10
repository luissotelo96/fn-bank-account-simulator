using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Repositories;

namespace BankAccountSimulation.Domain.Services
{
    public class FinancialProductService : IFinancialProductService
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        public FinancialProductService(IFinancialProductRepository financialProductRepository)
        {
            _financialProductRepository = financialProductRepository;
        }

        public async Task<int> CancelProduct(int financialProductId)
        {
            return await _financialProductRepository.CancelProduct(financialProductId);
        }

        public async Task<int> CreateFinancialProduct(FinancialProductDTO financialProduct)
        {
            return await _financialProductRepository.CreateFinancialProduct(financialProduct);
        }

        public async Task<int> DepositMoney(int financialProductId, decimal value)
        {
            return await _financialProductRepository.DepositMoney(financialProductId, value);
        }

        public async Task<List<AverageBalanceTable>> GetAverageBalanceByCustomer(string documentNumber)
        {
            return await _financialProductRepository.GetAverageBalanceByCustomer(documentNumber);
        }

        public async Task<FinancialProductDTO> GetFinancialMovementsByFinancialProductId(int financialProductId)
        {
            return await _financialProductRepository.GetFinancialMovementsByFinancialProductId(financialProductId);
        }

        public async Task<List<FinancialProductDTO>> GetFinancialProductsByCustomerID(int customerId)
        {
            return await _financialProductRepository.GetFinancialProductsByCustomerID(customerId);
        }

        public async Task<List<ProductTypeDTO>> GetProductTypes()
        {
            return await _financialProductRepository.GetProductTypes();
        }

        public async Task<List<TopBalanceCustomersTable>> GetTopBalanceCustomers()
        {
            return await _financialProductRepository.GetTopBalanceCustomers();
        }

        public async Task<int> WithdrawMoney(int financialProductId, decimal value)
        {
            return await _financialProductRepository.WithdrawMoney(financialProductId, value);
        }
    }
}
