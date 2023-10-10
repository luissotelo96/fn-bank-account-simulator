using BankAccountSimulation.DataAccess.Database;
using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Entities;
using BankAccountSimulation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankAccountSimulation.DataAccess.Repositories
{
    public class FinancialProductSqlRepository : IFinancialProductRepository
    {
        private readonly BankAccountSimulatorDbContext _dbContext;

        public FinancialProductSqlRepository(BankAccountSimulatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CancelProduct(int financialProductId)
        {
            var financialProductBd = await _dbContext.FinancialProduct
                                                    .Where(p => p.FinancialProductID == financialProductId)
                                                    .FirstOrDefaultAsync();
            if (financialProductBd != null)
            {
                // Se consulta si el cliente tiene una cuenta de ahorros, es decir, productTypeId = 1
                var financialProductAhorros = await _dbContext.FinancialProduct
                                                        .Where(p => p.CustomerID == financialProductBd.CustomerID && p.ProductTypeID == 1)
                                                        .FirstOrDefaultAsync();
                if (financialProductAhorros != null)
                {
                    financialProductAhorros.Balance += financialProductBd.Balance;
                    financialProductAhorros.MontlyInterest += financialProductBd.MontlyInterest;
                    financialProductBd.State = false;

                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    throw new Exception("Debe crear una cuenta de ahorros antes de cancelar la cuenta actual");
                }
            }
            else
            {
                throw new Exception("Producto no existe");
            }
        }

        public async Task<int> CreateFinancialProduct(FinancialProductDTO financialProduct)
        {
            var customerProductTypeCount = await _dbContext.FinancialProduct
                                                .Where(p => p.CustomerID == financialProduct.CustomerID && p.ProductTypeID == financialProduct.ProductTypeID && p.State)
                                                .CountAsync();
            if (customerProductTypeCount > 0)
            {
                throw new Exception("No puede tener más de un producto de este tipo.");
            }
            else
            {
                var financialProductEntity = MapFinancialProductToEntity(financialProduct);
                financialProductEntity.DateInit = DateTime.Now;
                financialProductEntity.State = true;

                _dbContext.FinancialProduct.Add(financialProductEntity);
                return await _dbContext.SaveChangesAsync();
            }


        }

        public async Task<int> DepositMoney(int financialProductId, decimal value)
        {
            var financialProductBd = await _dbContext.FinancialProduct.Where(p => p.FinancialProductID == financialProductId).FirstOrDefaultAsync();
            if (financialProductBd != null)
            {
                financialProductBd.Balance += value;


                var financialMovementEntity = new FinancialMovements()
                {
                    FinancialProductID = financialProductId,
                    CustomerID = financialProductBd.CustomerID,
                    MovementDate = DateTime.Now,
                    MovementType = "+",
                    Value = value,
                    Balance = financialProductBd.Balance
                };

                _dbContext.FinancialMovements.Add(financialMovementEntity);
                await _dbContext.SaveChangesAsync();
                return 1;

            }
            else
            {
                throw new Exception("Producto no existe");
            }
        }

        public Task<List<AverageBalanceTable>> GetAverageBalanceByCustomer(string documentNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<FinancialProductDTO> GetFinancialMovementsByFinancialProductId(int financialProductId)
        {
            var financialProduct = await _dbContext.FinancialProduct
                                                .Where(p => p.FinancialProductID == financialProductId)
                                                .Select(p => new FinancialProductDTO()
                                                {
                                                    FinancialProductID = p.FinancialProductID,
                                                    ProductTypeID = p.ProductTypeID,
                                                    Balance = p.Balance,
                                                    CustomerID = p.CustomerID,
                                                    DateInit = p.DateInit,
                                                    MontlyInterest = p.MontlyInterest,


                                                }).FirstOrDefaultAsync();

            if (financialProduct != null)
            {
                financialProduct.FinancialMovements = await _dbContext.FinancialMovements
                                    .Where(m => m.FinancialProductID == financialProductId)
                                    .Select(m => new FinancialMovementDTO()
                                    {
                                        FinancialMovementsID = m.FinancialMovementsID,
                                        FinancialProductID = m.FinancialProductID,
                                        MovementDate = m.MovementDate,
                                        MovementType = m.MovementType,
                                        Value = m.Value,
                                        Balance = m.Balance,
                                        CustomerID = m.CustomerID
                                    }).OrderByDescending(m => m.FinancialProductID).ToListAsync();

                financialProduct.ProductType = await _dbContext.ProductType.Where(pt => pt.ProductTypeID == financialProduct.ProductTypeID)
                                                        .Select(pt => new ProductTypeDTO()
                                                        {
                                                            ProductTypeID = pt.ProductTypeID,
                                                            Description = pt.Description,
                                                            MontlyInterest = pt.MontlyInterest
                                                        }).FirstOrDefaultAsync();
                return financialProduct;
            }
            else
            {
                throw new Exception("Producto no encontrado");
            }
        }

        public async Task<List<FinancialProductDTO>> GetFinancialProductsByCustomerID(int customerId)
        {
            return await _dbContext.FinancialProduct
                .Where(p => p.CustomerID == customerId && p.State)
                .Select(p => new FinancialProductDTO
                {
                    FinancialProductID = p.FinancialProductID,
                    ProductTypeID = p.ProductTypeID,
                    Balance = p.Balance,
                    CustomerID = customerId,
                    DateInit = p.DateInit,
                    ProductType = _dbContext.ProductType.Where(pt => pt.ProductTypeID == p.ProductTypeID)
                                                        .Select(pt => new ProductTypeDTO()
                                                        {
                                                            ProductTypeID = pt.ProductTypeID,
                                                            Description = pt.Description,
                                                            MontlyInterest = pt.MontlyInterest
                                                        }).FirstOrDefault()
                }).ToListAsync();
        }

        public async Task<List<ProductTypeDTO>> GetProductTypes()
        {
            return await _dbContext.ProductType
                            .Where(p => p.State == 1)
                            .Select(p => new ProductTypeDTO()
                            {
                                ProductTypeID = p.ProductTypeID,
                                Description = p.Description,
                                MontlyInterest = p.MontlyInterest
                            }).ToListAsync();
        }

        public Task<List<TopBalanceCustomersTable>> GetTopBalanceCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<int> WithdrawMoney(int financialProductId, decimal value)
        {
            var financialProductBd = await _dbContext.FinancialProduct
                                               .Where(p => p.FinancialProductID == financialProductId)
                                               .FirstAsync();
            if (financialProductBd != null)
            {
                financialProductBd.Balance -= value;
                var financialMovementEntity = new FinancialMovements()
                {
                    FinancialProductID = financialProductId,
                    CustomerID = financialProductBd.CustomerID,
                    MovementDate = DateTime.Now,
                    MovementType = "-",
                    Value = value,
                    Balance = financialProductBd.Balance
                };

                _dbContext.FinancialMovements.Add(financialMovementEntity);
                await _dbContext.SaveChangesAsync();
            }

            return 1;
        }


        private FinancialProduct MapFinancialProductToEntity(FinancialProductDTO financialProductDTO)
        {
            return new FinancialProduct()
            {
                FinancialProductID = financialProductDTO.FinancialProductID,
                Balance = financialProductDTO.Balance,
                ProductTypeID = financialProductDTO.ProductTypeID,
                CustomerID = financialProductDTO.CustomerID,
                MontlyInterest = financialProductDTO.MontlyInterest
            };
        }
    }
}
