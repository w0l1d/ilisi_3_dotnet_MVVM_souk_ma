using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Models;
using System.Configuration;

namespace AmazonShoping.Data
{
    public class SoukMVVMContext : DbContext
    {
        public SoukMVVMContext(DbContextOptions<SoukMVVMContext> options)
            : base(options)
        {
        }

        public DbSet<AmazonShoping.Models.Product> Product { get; set; } = default!;


        public DbSet<AmazonShoping.Models.Category>? Category { get; set; }


        public DbSet<AmazonShoping.Models.Order>? Order { get; set; }
    }
}