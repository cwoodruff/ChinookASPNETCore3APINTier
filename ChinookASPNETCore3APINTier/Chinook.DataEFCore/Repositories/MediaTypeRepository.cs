using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class MediaTypeRepository : EfRepository<Album>
    {
        public MediaTypeRepository(ChinookContext context) : base(context)
        {
        }
    }
}