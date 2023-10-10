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
    public class CustomerFunction
    {
        private readonly ICustomerService _customerService;

        public CustomerFunction(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [FunctionName("GetCustomerTypes")]
        public async Task<IActionResult> GetCustomerTypes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<CustomerTypeDTO> result = await _customerService.GetCustomerTypes();
                return new OkObjectResult(new Result<List<CustomerTypeDTO>> { IsSuccess = true, Message = "Exito", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CustomerDTO data = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);

                var result = await _customerService.CreateCustomer(data);

                if (result == 1)
                    return new OkObjectResult(new Result<int> { IsSuccess = true, Message = "Cliente guardado exitosamente.", Data = result });
                else
                    return new BadRequestObjectResult(new Result<int> { IsSuccess = false, Message = "Cliente no guardado.", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        [FunctionName("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                int customerId = jsonData.GetValue("CustomerId").Value<int>();

                CustomerDTO result = await _customerService.GetCustomerById(customerId);                
                return new OkObjectResult(new Result<CustomerDTO> { IsSuccess = true, Message = "Exito", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        [FunctionName("GetCustomers")]
        public async Task<IActionResult> GetCustomers(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetCustomers")] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<CustomerDTO> result = await _customerService.GetCustomers();
                return new OkObjectResult(new Result<List<CustomerDTO>> { IsSuccess = true, Message = "Exito", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }

        [FunctionName("GetCustomerByDocumentNumber")]
        public async Task<IActionResult> GetCustomerByDocumentNumber(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);
                string documentNumber = jsonData.GetValue("documentNumber").Value<string>();

                CustomerDTO result = await _customerService.GetCustomerByDocumentNumber(documentNumber);
                return new OkObjectResult(new Result<CustomerDTO> { IsSuccess = true, Message = "Exito", Data = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new Result<string> { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
