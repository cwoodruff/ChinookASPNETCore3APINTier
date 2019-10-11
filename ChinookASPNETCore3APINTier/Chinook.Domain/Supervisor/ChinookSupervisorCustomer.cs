    using System;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Chinook.Domain.Extensions;
    using Chinook.Domain.ApiModels;
    using Chinook.Domain.Entities;
    using Microsoft.Extensions.Caching.Memory;

    namespace Chinook.Domain.Supervisor
    {
        public partial class ChinookSupervisor
        {
            public IEnumerable<CustomerApiModel> GetAllCustomer()
            {
                var customers = _customerRepository.GetAll();
                foreach (var customer in customers)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(customer.CustomerId, customer, cacheEntryOptions);
                }
                return customers.ConvertAll();
            }

            public CustomerApiModel GetCustomerById(int id)
            {
                var customer = _cache.Get<Customer>(id);

                if (customer != null)
                {
                    var customerApiModel = customer.Convert;
                    customerApiModel.Invoices = (GetInvoiceByCustomerId(customerApiModel.CustomerId)).ToList();
                    customerApiModel.SupportRep =
                        GetEmployeeById(customerApiModel.SupportRepId.GetValueOrDefault());
                    customerApiModel.SupportRepName =
                        $"{customerApiModel.SupportRep.LastName}, {customerApiModel.SupportRep.FirstName}";
                    return customerApiModel;
                }
                else
                {
                    var customerApiModel = (_customerRepository.GetById(id)).Convert;
                    customerApiModel.Invoices = (GetInvoiceByCustomerId(customerApiModel.CustomerId)).ToList();
                    customerApiModel.SupportRep =
                        GetEmployeeById(customerApiModel.SupportRepId.GetValueOrDefault());
                    customerApiModel.SupportRepName =
                        $"{customerApiModel.SupportRep.LastName}, {customerApiModel.SupportRep.FirstName}";

                    var cacheEntryOptions =
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(customerApiModel.CustomerId, customerApiModel, cacheEntryOptions);

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

                var customer = newCustomerApiModel.Convert;

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