using BankAccountSimulation.DataAccess.Database;
using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Entities;
using BankAccountSimulation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankAccountSimulation.DataAccess.Repositories
{
    public class CustomerSqlRepository : ICustomerRepository
    {
        private readonly BankAccountSimulatorDbContext _dbContext;

        public CustomerSqlRepository(BankAccountSimulatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCustomer(CustomerDTO newCustomer)
        {
            var customerBd = await _dbContext.Customer.Where(c => c.DocumentNumber == newCustomer.DocumentNumber).FirstOrDefaultAsync();

            if (customerBd == null)
            {
                var customerEntity = MapCustomerDtoToEntity(newCustomer);
                customerEntity.State = true;

                if (newCustomer.LegalRepresentative != null)
                {
                    var legalRepresentativeEntity = MapCustomerDtoToEntity(newCustomer.LegalRepresentative);
                    _dbContext.Customer.Add(legalRepresentativeEntity);
                    await _dbContext.SaveChangesAsync();
                    customerEntity.LegalRepresentativeID = legalRepresentativeEntity.CustomerID;
                }

                _dbContext.Customer.Add(customerEntity);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            else
            {
                throw new Exception("Ya existe un cliente con el mismo número de documento");
            }
           
        }

        public async Task<CustomerDTO> GetCustomerByDocumentNumber(string documentNumber)
        {
            return await _dbContext.Customer
                .Where(c => c.DocumentNumber == documentNumber && c.State)
                .Select(c => new CustomerDTO()
                {
                    CustomerID = c.CustomerID,
                    DocumentNumber = c.DocumentNumber,
                    Name = c.Name,
                    CustomerType = _dbContext.CustomerType.Where(ct => ct.CustomerTypeID == c.CustomerTypeID).Select(ct => new CustomerTypeDTO()
                    {
                        CustomerTypeID = ct.CustomerTypeID,
                        Description = ct.Description
                    }).FirstOrDefault()
                }).FirstOrDefaultAsync() ?? new CustomerDTO();
        }

        public async Task<CustomerDTO> GetCustomerById(int customerId)
        {
            var customer = await _dbContext.Customer
                            .Where(c => c.CustomerID == customerId)
                            .Select(c => new CustomerDTO()
                            {
                                CustomerID = c.CustomerID,
                                DocumentNumber = c.DocumentNumber,
                                Name = c.Name,
                                CustomerType = _dbContext.CustomerType.Where(ct => ct.CustomerTypeID == c.CustomerTypeID).Select(ct => new CustomerTypeDTO()
                                {
                                    CustomerTypeID = ct.CustomerTypeID,
                                    Description = ct.Description
                                }).FirstOrDefault(),
                                LegalRepresentativeID = c.LegalRepresentativeID,
                                PhoneNumber = c.PhoneNumber,
                            }).FirstOrDefaultAsync();

            if (customer != null)
            {
                if (customer.LegalRepresentativeID != null)
                {
                    customer.LegalRepresentative = await GetCustomerById((int)customer.LegalRepresentativeID);
                }
                return customer;
            }
            else
            {
                throw new Exception("El cliente no existe");
            }


        }

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            return await _dbContext.Customer.Where(c => c.State)
                                            .Select(c => new CustomerDTO()
                                            {
                                                CustomerID = c.CustomerID,
                                                DocumentNumber = c.DocumentNumber,
                                                Name = c.Name,
                                                CustomerType = _dbContext.CustomerType.Where(ct => ct.CustomerTypeID == c.CustomerTypeID).Select(ct => new CustomerTypeDTO()
                                                {
                                                    CustomerTypeID = ct.CustomerTypeID,
                                                    Description = ct.Description
                                                }).FirstOrDefault()
                                            }).ToListAsync();
        }

        public async Task<List<CustomerTypeDTO>> GetCustomerTypes()
        {
            return await _dbContext.CustomerType.Where(c => c.State == 1).Select(c => new CustomerTypeDTO()
            {
                CustomerTypeID = c.CustomerTypeID,
                Description = c.Description,
            }).ToListAsync();
        }

        private Customer MapCustomerDtoToEntity(CustomerDTO customerDTO)
        {
            return new Customer()
            {
                CustomerID = customerDTO.CustomerID,
                DocumentNumber = customerDTO.DocumentNumber,
                Name = customerDTO.Name,
                CustomerTypeID = customerDTO.CustomerType?.CustomerTypeID,
                PhoneNumber = customerDTO.PhoneNumber,
            };
        }
    }
}
