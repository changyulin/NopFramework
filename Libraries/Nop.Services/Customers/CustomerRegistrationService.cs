using Nop.Core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly ICustomerService _customerService;

        public CustomerRegistrationService(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public CustomerLoginResultsEnum ValidateCustomer(string usernameOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request)
        {
            var result = new CustomerRegistrationResult();
            request.Customer.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Registered);
            if (registeredRole == null)
                throw new Exception("'Registered' role could not be loaded");
            request.Customer.CustomerRoles.Add(registeredRole);
            //remove from 'Guests' role
            var guestRole = request.Customer.CustomerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Guests);
            if (guestRole != null)
                request.Customer.CustomerRoles.Remove(guestRole);

            _customerService.UpdateCustomer(request.Customer);
            return result;
        }
    }
}
