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
using STC.API.Entities.ComponentEntity;
using STC.API.Entities.OpportunityEntity;

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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ComponentVersion> ComponentVersions { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }

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

            modelBuilder.Entity<ComponentType>()
                .HasOne(o => o.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Opportunity>()
                .HasOne(o => o.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Opportunity>()
                .HasOne(o => o.ModifiedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Opportunity>()
                .HasMany(o => o.Components)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.Opportunity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.ComponentType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.AccountExecutive)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.SolutionsArchitect)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.ModifiedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Component>()
                .HasOne(o => o.Stage)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
               

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.Component)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.Opportunity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.ComponentType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.AccountExecutive)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.SolutionsArchitect)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.ModifiedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentVersion>()
                .HasOne(o => o.Stage)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
