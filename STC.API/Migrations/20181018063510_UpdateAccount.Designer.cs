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
    [Migration("20181018063510_UpdateAccount")]
    partial class UpdateAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
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

            modelBuilder.Entity("STC.API.Entities.GroupEntity.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("STC.API.Entities.GroupEntity.GroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMembers");
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

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("Priority");

                    b.Property<int>("RequesterId");

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

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketProducts");
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketResolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cause");

                    b.Property<bool>("Closed");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Solution");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketResolutions");
                });

            modelBuilder.Entity("STC.API.Entities.Users.User", b =>
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

                    b.Property<int?>("GroupId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("ObjectId")
                        .IsRequired();

                    b.Property<int?>("RoleId");

                    b.Property<int?>("SupervisorId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("STC.API.Entities.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("STC.API.Entities.GroupEntity.GroupMember", b =>
                {
                    b.HasOne("STC.API.Entities.GroupEntity.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.Users.User", "User")
                        .WithMany()
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

                    b.HasOne("STC.API.Entities.Users.User", "Assignee")
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
                    b.HasOne("STC.API.Entities.Users.User", "CreatedBy")
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
                    b.HasOne("STC.API.Entities.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.TicketEntity.Ticket", "Ticket")
                        .WithMany("Procedures")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketProduct", b =>
                {
                    b.HasOne("STC.API.Entities.ProductEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.TicketEntity.Ticket", "Ticket")
                        .WithMany("Products")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.TicketEntity.TicketResolution", b =>
                {
                    b.HasOne("STC.API.Entities.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("STC.API.Entities.TicketEntity.Ticket", "Ticket")
                        .WithMany("Resolutions")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("STC.API.Entities.Users.User", b =>
                {
                    b.HasOne("STC.API.Entities.GroupEntity.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("STC.API.Entities.Users.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("STC.API.Entities.Users.User", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId");
                });
#pragma warning restore 612, 618
        }
    }
}
