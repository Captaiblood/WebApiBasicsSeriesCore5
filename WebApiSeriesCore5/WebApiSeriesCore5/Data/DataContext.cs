
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Entities;

namespace WebApiSeriesCore5.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

        public DbSet <Entities.Company>  Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
    }
}
