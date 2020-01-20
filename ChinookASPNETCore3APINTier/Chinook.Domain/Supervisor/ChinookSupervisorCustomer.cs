    using System;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Chinook.Domain.ApiModels;
    using Chinook.Domain.Extensions;
    using Microsoft.Extensions.Caching.Memory;

    namespace Chinook.Domain.Supervisor
    {
        public partial class ChinookSupervisor
        {
            public bool CustomerExists(int id) => _customerRepository.CustomerExists(id);
            public async Task<IAsyncEnumerable<CustomerApiModel>> GetAllCustomer()
            {
                var customers = (await _customerRepository.GetAll()).ConvertAll();
                await foreach (var customer in customers)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(string.Concat("Customer-", customer.CustomerId), customer, cacheEntryOptions);
                }
                return customers;
            }

            public async Task<CustomerApiModel> GetCustomerById(int id)
            {
                var customerApiModelCached = _cache.Get<CustomerApiModel>(string.Concat("Customer-", id));

                if (customerApiModelCached != null)
                {
                    return customerApiModelCached;
                }
                else
                {
                    var customerApiModel = (await _customerRepository.GetById(id)).Convert();
                    var cacheEntryOptions =
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(string.Concat("Customer-", customerApiModel.CustomerId), customerApiModel, cacheEntryOptions);

                    return customerApiModel;
                }
            }

            public async Task<IAsyncEnumerable<CustomerApiModel>> GetCustomerBySupportRepId(int id)
            {
                var customers = await _customerRepository.GetBySupportRepId(id);
                return customers.ConvertAll();
            }

            public async Task<CustomerApiModel> AddCustomer(CustomerApiModel newCustomerApiModel)
            {
                var customer = newCustomerApiModel.Convert();
                customer = await _customerRepository.Add(customer);
                return customer.Convert();
            }

            public async Task<bool> UpdateCustomer(CustomerApiModel customerApiModel) => await _customerRepository.Update(customerApiModel.Convert());

            public async Task<bool> DeleteCustomer(int id) => await _customerRepository.Delete(id);
        }
    }