using BankAccountSimulation.Domain.DTO;

namespace BankAccountSimulation.Domain.Repositories
{
    public interface IFinancialProductRepository
    {
        Task<List<ProductTypeDTO>> GetProductTypes();
        Task<int> CreateFinancialProduct(FinancialProductDTO financialProduct);
        Task<List<FinancialProductDTO>> GetFinancialProductsByCustomerID(int customerId);
        Task<int> WithdrawMoney(int financialProductId, decimal value);
        Task<int> DepositMoney(int financialProductId, decimal value);
        Task<List<AverageBalanceTable>> GetAverageBalanceByProductTypeId(int productTypeId);
        Task<List<TopBalanceCustomersTable>> GetTopBalanceCustomers(int productTypeId);
        Task<FinancialProductDTO> GetFinancialMovementsByFinancialProductId(int financialProductId);
        Task<int> CancelProduct (int financialProductId);
    }
}
