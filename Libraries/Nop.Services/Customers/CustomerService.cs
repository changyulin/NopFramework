using Nop.Core.Domain.Customers;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;

        public CustomerService(IRepository<Customer> customerRepository,
            IRepository<CustomerRole> customerRoleRepository)
        {
            this._customerRepository = customerRepository;
            this._customerRoleRepository = customerRoleRepository;
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            customer.Deleted = true;
            if (!String.IsNullOrEmpty(customer.Email))
                customer.Email += "-DELETED";
            if (!String.IsNullOrEmpty(customer.Username))
                customer.Username += "-DELETED";
            UpdateCustomer(customer);
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return _customerRepository.GetById(customerId);
        }

        public IList<Customer> GetCustomersByIds(int[] customerIds)
        {
            if (customerIds == null || customerIds.Length == 0)
                return new List<Customer>();

            var query = from c in _customerRepository.Table
                        where customerIds.Contains(c.Id)
                        select c;
            var customers = query.ToList();
            //sort by passed identifiers
            var sortedCustomers = new List<Customer>();
            foreach (int id in customerIds)
            {
                var customer = customers.Find(x => x.Id == id);
                if (customer != null)
                    sortedCustomers.Add(customer);
            }
            return sortedCustomers;
        }

        public Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;
            return _customerRepository.Table.Where(c => c.CustomerGuid == customerGuid).FirstOrDefault();
        }

        public Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            return _customerRepository.Table.Where(c => c.Email == email).FirstOrDefault();
        }

        public Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            return _customerRepository.Table.Where(c => c.Username == username).FirstOrDefault();
        }

        public Customer InsertGuestCustomer()
        {
            throw new NotImplementedException();
        }

        public void InsertCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomerRole(CustomerRole customerRole)
        {
            throw new NotImplementedException();
        }

        public CustomerRole GetCustomerRoleById(int customerRoleId)
        {
            throw new NotImplementedException();
        }

        public CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerRole> GetAllCustomerRoles(bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public void InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Insert(customerRole);
        }

        public void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Update(customerRole);
        }
    }
}
