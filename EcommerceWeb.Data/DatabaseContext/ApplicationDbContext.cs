﻿using Microsoft.EntityFrameworkCore;
using EcommerceWeb.Data.Entities;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.CodeAnalysis.Differencing;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcommerceWeb.Data.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> userInfos { get; set; }
        public DbSet<ProductImage> productImages { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the DateCreated properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).DateCreated = DateTime.UtcNow;
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the properties
                    // so we set their state as IsModified to false
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.DateCreated).IsModified = false;
                }

                // In any case we always want to set the properties
                // DateUpdated
                ((AuditableEntity)entityEntry.Entity).DateUpdated = DateTime.UtcNow;
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
