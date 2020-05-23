using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class CustomerRepository : EfRepository<Customer>
    {
        public CustomerRepository(ChinookContext context) : base(context)
        {
        }

        public List<Customer> GetBySupportRepId(int id) => _dbContext.Customer.Where(a => a.SupportRepId == id).ToList();
    }
}