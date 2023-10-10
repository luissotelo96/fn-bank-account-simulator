using BankAccountSimulation.Domain.DTO;

namespace BankAccountSimulation.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerDTO> GetCustomerById(int customerId);
        Task<int> CreateCustomer(CustomerDTO newCustomer);
        Task<List<CustomerTypeDTO>> GetCustomerTypes();
        Task<CustomerDTO> GetCustomerByDocumentNumber(string documentNumber);
        Task<List<CustomerDTO>> GetCustomers();
    }
}
