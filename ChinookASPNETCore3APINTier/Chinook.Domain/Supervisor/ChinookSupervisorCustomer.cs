    using System;
    
    using System.Collections.Generic;
    using System.Linq;
    using Chinook.Domain.Extensions;
    using Chinook.Domain.ApiModels;
    using Microsoft.Extensions.Caching.Memory;

    namespace Chinook.Domain.Supervisor
    {
        public partial class ChinookSupervisor
        {
            public IEnumerable<CustomerApiModel> GetAllCustomer()
            {
                var customers = _customerRepository.GetAll().ConvertAll();
                foreach (var customer in customers)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(string.Concat("Customer-", customer.CustomerId), customer, cacheEntryOptions);
                }
                return customers;
            }

            public CustomerApiModel GetCustomerById(int id)
            {
                var customerApiModelCached = _cache.Get<CustomerApiModel>(string.Concat("Customer-", id));

                if (customerApiModelCached != null)
                {
                    return customerApiModelCached;
                }
                else
                {
                    var customerApiModel = (_customerRepository.GetById(id)).Convert();
                    customerApiModel.Invoices = (GetInvoiceByCustomerId(customerApiModel.CustomerId)).ToList();
                    customerApiModel.SupportRep =
                        GetEmployeeById(customerApiModel.SupportRepId.GetValueOrDefault());
                    customerApiModel.SupportRepName =
                        $"{customerApiModel.SupportRep.LastName}, {customerApiModel.SupportRep.FirstName}";

                    var cacheEntryOptions =
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(string.Concat("Customer-", customerApiModel.CustomerId), customerApiModel, cacheEntryOptions);

                    return customerApiModel;
                }
            }

            public IEnumerable<CustomerApiModel> GetCustomerBySupportRepId(int id)
            {
                var customers = _customerRepository.GetBySupportRepId(id);
                return customers.ConvertAll();
            }

            public CustomerApiModel AddCustomer(CustomerApiModel newCustomerApiModel)
            {
                /*var customer = new Customer
                {
                    FirstName = newCustomerApiModel.FirstName,
                    LastName = newCustomerApiModel.LastName,
                    Company = newCustomerApiModel.Company,
                    Address = newCustomerApiModel.Address,
                    City = newCustomerApiModel.City,
                    State = newCustomerApiModel.State,
                    Country = newCustomerApiModel.Country,
                    PostalCode = newCustomerApiModel.PostalCode,
                    Phone = newCustomerApiModel.Phone,
                    Fax = newCustomerApiModel.Fax,
                    Email = newCustomerApiModel.Email,
                    SupportRepId = newCustomerApiModel.SupportRepId
                };*/

                var customer = newCustomerApiModel.Convert();

                customer = _customerRepository.Add(customer);
                newCustomerApiModel.CustomerId = customer.CustomerId;
                return newCustomerApiModel;
            }

            public bool UpdateCustomer(CustomerApiModel customerApiModel)
            {
                var customer = _customerRepository.GetById(customerApiModel.CustomerId);

                if (customer == null) return false;
                customer.FirstName = customerApiModel.FirstName;
                customer.LastName = customerApiModel.LastName;
                customer.Company = customerApiModel.Company;
                customer.Address = customerApiModel.Address;
                customer.City = customerApiModel.City;
                customer.State = customerApiModel.State;
                customer.Country = customerApiModel.Country;
                customer.PostalCode = customerApiModel.PostalCode;
                customer.Phone = customerApiModel.Phone;
                customer.Fax = customerApiModel.Fax;
                customer.Email = customerApiModel.Email;
                customer.SupportRepId = customerApiModel.SupportRepId;

                return _customerRepository.Update(customer);
            }

            public bool DeleteCustomer(int id) 
                => _customerRepository.Delete(id);
        }
    }