using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Models;
using System.Configuration;

namespace AmazonShoping.Data
{
    public class AmazonCLoneContextSQLite : DbContext
    {

        protected readonly IConfiguration Configuration;
        public AmazonCLoneContextSQLite(IConfiguration configuration)
        {
            Configuration = configuration;
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("AmazonCLoneContextSQLite"));
        }


        public DbSet<AmazonShoping.Models.Product> Product { get; set; } = default!;


        public DbSet<AmazonShoping.Models.Category>? Category { get; set; }


        // public DbSet<AmazonShoping.Models.Order>? Order { get; set; }


    }
}
