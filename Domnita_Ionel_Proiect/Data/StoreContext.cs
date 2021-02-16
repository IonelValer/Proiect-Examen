using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domnita_Ionel_Proiect.Models;

namespace Domnita_Ionel_Proiect.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<OnlineStore> OnlineStore { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Phone>().ToTable("Phone");
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<OnlineStore>().ToTable("OnlineStore");
            modelBuilder.Entity<OnlineStore>()
            .HasKey(c => new { c.PhoneID, c.StoreID });//configureaza cheia primara compusa
        }
    }
}
