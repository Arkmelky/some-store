using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.DbAccess
{
    internal class EFDbContext : DbContext
    {
        public EFDbContext():base("SomeStore_db")
        {
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<StoreProduct> SotreProducts { get; set; }
        
    }
}
