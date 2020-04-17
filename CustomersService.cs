using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserID == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";

            }
            else if (request.UserID == 2)
            {
                output.FirstName = "Jan";
                output.LastName = "Doe";

            }
            else if (request.UserID == 3)
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";

            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomerRequest(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Corye",
                    EmailAdress = "test@gmail.com",
                    Age = 21,
                    IsAlive = true
                
                },

                 new CustomerModel
                {
                    FirstName = "Ruby",
                    LastName = "Nha khang",
                    EmailAdress = "Ruby@gmail.com",
                    Age = 21,
                    IsAlive = true

                },

                  new CustomerModel
                {
                    FirstName = "Tom",
                    LastName = "Jerry",
                    EmailAdress = "TomJerry@gmail.com",
                    Age = 21,
                    IsAlive = true

                },
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            
            }

        }






    }
}
