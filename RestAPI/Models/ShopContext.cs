using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RestAPI.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; }
  
    }
}
