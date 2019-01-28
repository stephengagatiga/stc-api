﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STC.API.Data;

namespace STC.API.Migrations
{
    [DbContext(typeof(STCDbContext))]
    [Migration("20190117035621_AddOpportunity")]
    partial class AddOpportunity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("STC.API.Entities.AccountEntity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountIndustryId");

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<string>("ContactDetails");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("TermsOfPayment");

                    b.HasKey("Id");

                    b.HasIndex("AccountIndustryId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("STC.API.Entities.AccountEntity.AccountContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<bool>("Active");

                    b.Property<string>("ContactDetails")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Designation");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("PrimaryContact");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountContacts");
                });

            modelBuilder.Entity("STC.API.Entities.AccountEntity.AccountIndustry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AccountIndustries");
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountExecutiveId");

                    b.Property<int>("CategoryId");

                    b.Property<int>("ComponentTypeId");

                    b.Property<decimal>("CostPerUnit")
                        .HasColumnType("decimal(12, 4)");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("ModifiedById");

                    b.Property<int>("OpportunityId");

                    b.Property<bool>("POC");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(12, 4)");

                    b.Property<int>("ProductId");

                    b.Property<int>("Qty");

                    b.Property<string>("Remarks");

                    b.Property<int>("SolutionsArchitectId");

                    b.Property<int>("StageId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TargetCloseMonth");

                    b.Property<DateTime>("Validity");

                    b.Property<int>("VersionNumber");

                    b.HasKey("Id");

                    b.HasIndex("AccountExecutiveId")
                        .IsUnique();

                    b.HasIndex("CategoryId");

                    b.HasIndex("ComponentTypeId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("OpportunityId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SolutionsArchitectId");

                    b.HasIndex("StageId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.ComponentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ComponentTypes");
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.ComponentVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountExecutiveId");

                    b.Property<DateTime>("Added");

                    b.Property<int>("CategoryId");

                    b.Property<int>("ComponentId");

                    b.Property<int?>("ComponentId1");

                    b.Property<int>("ComponentTypeId");

                    b.Property<decimal>("CostPerUnit")
                        .HasColumnType("decimal(12, 4)");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("ModifiedById");

                    b.Property<int>("OpportunityId");

                    b.Property<bool>("POC");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(12, 4)");

                    b.Property<int>("ProductId");

                    b.Property<int>("Qty");

                    b.Property<string>("Remarks");

                    b.Property<int>("SolutionsArchitectId");

                    b.Property<int>("StageId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TargetCloseMonth");

                    b.Property<DateTime>("Validity");

                    b.Property<int>("VersionNumber");

                    b.HasKey("Id");

                    b.HasIndex("AccountExecutiveId")
                        .IsUnique();

                    b.HasIndex("CategoryId");

                    b.HasIndex("ComponentId");

                    b.HasIndex("ComponentId1");

                    b.HasIndex("ComponentTypeId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("OpportunityId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SolutionsArchitectId");

                    b.HasIndex("StageId");

                    b.ToTable("ComponentVersions");
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name");

                    b.Property<int>("Percentage");

                    b.HasKey("Id");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("STC.API.Entities.GroupEntity.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("STC.API.Entities.OpportunityEntity.Opportunity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("BigDealCode");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("ModifiedById");

                    b.Property<string>("Status");

                    b.Property<int>("TotalQty");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Opportunities");
                });

            modelBuilder.Entity("STC.API.Entities.ProductAssignmentEntity.ProductAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductAssignments");
                });

            modelBuilder.Entity("STC.API.Entities.ProductEntity.Principal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Principals");
                });

            modelBuilder.Entity("STC.API.Entities.ProductEntity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("PrincipalId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("PrincipalId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int?>("AssigneeId");

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<DateTime?>("ClosedOn");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("Priority");

                    b.Property<string>("Products");

                    b.Property<int>("RequesterId");

                    b.Property<string>("Resolution");

                    b.Property<string>("RootCause");

                    b.Property<int>("Status");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketHistories");
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketProcedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketProcedures");
                });

            modelBuilder.Entity("STC.API.Entities.UserEntity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("ObjectId")
                        .IsRequired();

                    b.Property<int?>("RoleId");

                    b.Property<int?>("SupervisorId");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("STC.API.Entities.UserRoleEntity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("STC.API.Entities.AccountEntity.Account", b =>
                {
                    b.HasOne("STC.API.Entities.AccountEntity.AccountIndustry", "Industry")
                        .WithMany()
                        .HasForeignKey("AccountIndustryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.AccountEntity.AccountContact", b =>
                {
                    b.HasOne("STC.API.Entities.AccountEntity.Account", "Account")
                        .WithMany("AccountContacts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.Component", b =>
                {
                    b.HasOne("STC.API.Entities.UserEntity.User", "AccountExecutive")
                        .WithOne()
                        .HasForeignKey("STC.API.Entities.ComponentEntity.Component", "AccountExecutiveId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.ComponentType", "ComponentType")
                        .WithMany()
                        .HasForeignKey("ComponentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.OpportunityEntity.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ProductEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "SolutionsArchitect")
                        .WithMany()
                        .HasForeignKey("SolutionsArchitectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.ComponentType", b =>
                {
                    b.HasOne("STC.API.Entities.ComponentEntity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("STC.API.Entities.ComponentEntity.ComponentVersion", b =>
                {
                    b.HasOne("STC.API.Entities.UserEntity.User", "AccountExecutive")
                        .WithOne()
                        .HasForeignKey("STC.API.Entities.ComponentEntity.ComponentVersion", "AccountExecutiveId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.ComponentEntity.Component")
                        .WithMany("Versions")
                        .HasForeignKey("ComponentId1");

                    b.HasOne("STC.API.Entities.ComponentEntity.ComponentType", "ComponentType")
                        .WithMany()
                        .HasForeignKey("ComponentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.UserEntity.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.OpportunityEntity.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ProductEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "SolutionsArchitect")
                        .WithMany()
                        .HasForeignKey("SolutionsArchitectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.ComponentEntity.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("STC.API.Entities.OpportunityEntity.Opportunity", b =>
                {
                    b.HasOne("STC.API.Entities.AccountEntity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.UserEntity.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("STC.API.Entities.ProductAssignmentEntity.ProductAssignment", b =>
                {
                    b.HasOne("STC.API.Entities.ProductEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.UserEntity.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.ProductEntity.Principal", b =>
                {
                    b.HasOne("STC.API.Entities.GroupEntity.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("STC.API.Entities.ProductEntity.Product", b =>
                {
                    b.HasOne("STC.API.Entities.ProductEntity.Principal", "Principal")
                        .WithMany("Products")
                        .HasForeignKey("PrincipalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.Ticket", b =>
                {
                    b.HasOne("STC.API.Entities.AccountEntity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.UserEntity.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.AccountEntity.AccountContact", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketHistory", b =>
                {
                    b.HasOne("STC.API.Entities.UserEntity.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("STC.API.Entities.TicketEntity.Ticket", "Ticket")
                        .WithMany("Histories")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketProcedure", b =>
                {
                    b.HasOne("STC.API.Entities.UserEntity.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.TicketEntity.Ticket", "Ticket")
                        .WithMany("Procedures")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.UserEntity.User", b =>
                {
                    b.HasOne("STC.API.Entities.UserRoleEntity.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("STC.API.Entities.UserEntity.User", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId");
                });
#pragma warning restore 612, 618
        }
    }
}
