using STC.API.Entities.AccountEntity;
using STC.API.Entities.ProductEntity;
using STC.API.Entities.GroupEntity;
using STC.API.Entities.TicketEntity;
using STC.API.Entities.UserRoleEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using STC.API.Entities.UserEntity;
using STC.API.Entities.ProductAssignmentEntity;

namespace STC.API.Data
{
    public class STCDbContext : DbContext
    {
        public STCDbContext(DbContextOptions<STCDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAssignment> ProductAssignments { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountIndustry> AccountIndustries { get; set; }
        public DbSet<AccountContact> AccountContacts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }
        public DbSet<TicketProcedure> TicketProcedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.ObjectId)
                .IsUnique();

            modelBuilder.Entity<Principal>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Assignee)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Requester)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketHistory>()
                .HasOne(h => h.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
