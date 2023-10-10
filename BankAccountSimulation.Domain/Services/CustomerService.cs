using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Repositories;

namespace BankAccountSimulation.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> CreateCustomer(CustomerDTO newCustomer)
        {
            return await _customerRepository.CreateCustomer(newCustomer);
        }

        public async Task<CustomerDTO> GetCustomerByDocumentNumber(string documentNumber)
        {
            return await _customerRepository.GetCustomerByDocumentNumber(documentNumber);
        }

        public async Task<CustomerDTO> GetCustomerById(int customerId)
        {
            return await _customerRepository.GetCustomerById(customerId);
        }

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<List<CustomerTypeDTO>> GetCustomerTypes()
        {
            return await _customerRepository.GetCustomerTypes();
        }
    }
}
