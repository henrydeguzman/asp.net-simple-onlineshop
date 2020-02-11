using System;
using Microsoft.EntityFrameworkCore;

namespace asp.net_simple_onlineshop.Models
{
    public class AppDbContext: DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Pie> Pies { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
