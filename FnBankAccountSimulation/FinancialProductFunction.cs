using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FnBankAccountSimulation
{
    public class FinancialProductFunction
    {
        private readonly IFinancialProductService _financialProductService;

        public FinancialProductFunction(IFinancialProductService financialProductService)
        {
            _financialProductService = financialProductService;
        }

        /* 
         * Función encargada de devolver la lista de los tipo de productos existentes (Cuenta de ahorros, corriente, CDT).
         */
        [FunctionName("GetProductTypes")]
        public async Task<IActionResult> GetProductTypes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<ProductTypeDTO> result = await _financialProductService.GetProductTypes();
                return new OkObjectResult(new Result<List<ProductTypeDTO>> { IsSuccess = true, Message = "Exito", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /*
         * Función encargada de crear productos
         */
        [FunctionName("CreateFinancialProduct")]
        public async Task<IActionResult> CreateFinancialProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                FinancialProductDTO data = JsonConvert.DeserializeObject<FinancialProductDTO>(requestBody);

                var result = await _financialProductService.CreateFinancialProduct(data);

                if (result == 1)
                    return new OkObjectResult(new Result<int> { IsSuccess = true, Message = "Producto creado exitosamente.", Data = result });
                else
                    return new BadRequestObjectResult(new Result<int> { IsSuccess = false, Message = "Producto no creado.", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /* 
         * Depositar dinero 
         */
        [FunctionName("DepositMoney")]
        public async Task<IActionResult> DepositMoney(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int financialProductId = jsonData.GetValue("financialProductId").Value<int>();
                decimal value = jsonData.GetValue("value").Value<decimal>();

                var result = await _financialProductService.DepositMoney(financialProductId, value);

                if (result == 1)
                    return new OkObjectResult(new Result<int> { IsSuccess = true, Message = "Operación exitosa", Data = result });
                else
                    return new BadRequestObjectResult(new Result<int> { IsSuccess = false, Message = "Operación no exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /* 
         * Obtener el saldo promedio según el tipo de producto.
         */
        [FunctionName("GetAverageBalanceByProductTypeId")]
        public async Task<IActionResult> GetAverageBalanceByProductTypeId(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
          ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int productTypeId = jsonData.GetValue("productTypeId").Value<int>();

                List<AverageBalanceTable> result = await _financialProductService.GetAverageBalanceByProductTypeId(productTypeId);
                return new OkObjectResult(new Result<List<AverageBalanceTable>> { IsSuccess = true, Message = "Operación exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /*
         * 
         * Obtener los productos de un cliente
         */
        [FunctionName("GetFinancialProductsByCustomerID")]
        public async Task<IActionResult> GetFinancialProductsByCustomerID(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
          ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int customerId = jsonData.GetValue("customerId").Value<int>();

                List<FinancialProductDTO> result = await _financialProductService.GetFinancialProductsByCustomerID(customerId);
                return new OkObjectResult(new Result<List<FinancialProductDTO>> { IsSuccess = true, Message = "Operación exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /*
         * Obtener el TOP 10 saldo en clientes por tipo producto
         */
        [FunctionName("GetTopBalanceCustomers")]
        public async Task<IActionResult> GetTopBalanceCustomers(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
         ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int productTypeId = jsonData.GetValue("productTypeId").Value<int>();

                List<TopBalanceCustomersTable> result = await _financialProductService.GetTopBalanceCustomers(productTypeId);
                return new OkObjectResult(new Result<List<TopBalanceCustomersTable>> { IsSuccess = true, Message = "Operación exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /*
         * Retirar dinero 
         */
        [FunctionName("WithdrawMoney")]
        public async Task<IActionResult> WithdrawMoney(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
          ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int financialProductId = jsonData.GetValue("financialProductId").Value<int>();
                decimal value = jsonData.GetValue("value").Value<decimal>();

                var result = await _financialProductService.WithdrawMoney(financialProductId, value);

                if (result == 1)
                    return new OkObjectResult(new Result<int> { IsSuccess = true, Message = "Operación exitosa", Data = result });
                else
                    return new BadRequestObjectResult(new Result<int> { IsSuccess = false, Message = "Operación no exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /* 
         * Obtener los movimiento de un producto
         */
        [FunctionName("GetFinancialMovementsByFinancialProductId")]
        public async Task<IActionResult> GetFinancialMovementsByFinancialProductId(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
         ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int financialProductId = jsonData.GetValue("financialProductId").Value<int>();

                FinancialProductDTO result = await _financialProductService.GetFinancialMovementsByFinancialProductId(financialProductId);
                return new OkObjectResult(new Result<FinancialProductDTO> { IsSuccess = true, Message = "Operación exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        /* 
         *  Cancelar producto
         */
        [FunctionName("CancelProduct")]
        public async Task<IActionResult> CancelProduct(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int financialProductId = jsonData.GetValue("financialProductId").Value<int>();

                var result = await _financialProductService.CancelProduct(financialProductId);

                if (result == 1)
                    return new OkObjectResult(new Result<int> { IsSuccess = true, Message = "Operación exitosa", Data = result });
                else
                    return new BadRequestObjectResult(new Result<int> { IsSuccess = false, Message = "Operación no exitosa", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

    }
}
