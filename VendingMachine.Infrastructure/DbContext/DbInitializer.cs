using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.DbContext;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        this._builder = builder;
    }

    public void Seed()
    {

        _builder.Entity<IdentityRole<int>>()
                .HasData(new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

        _builder.Entity<IdentityRole<int>>()
                .HasData(new IdentityRole<int>
                {
                    Id = 2,
                    Name = "Seller",
                    NormalizedName = "SELLER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                });

        _builder.Entity<IdentityRole<int>>()
               .HasData(new IdentityRole<int>
               {
                   Id = 3,
                   Name = "Buyer",
                   NormalizedName = "BUYER",
                   ConcurrencyStamp = Guid.NewGuid().ToString()

               });





        _builder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                UserName = "buyer",
                NormalizedUserName = "BUYER",
                Email = "buyer@vm.com",
                NormalizedEmail = "BUYER@VM.COM",
                Deposit = 0,
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEMpf+HOcOP1h3YPp6oapYPAhSZylSZiPGjAOqATnWJjj2+Nncqc2rXS/IBn4gSyB0Q==",
                SecurityStamp = "MPLTHNGG7JU3YGBA5O6I7LBFRXEBP6N6",
                ConcurrencyStamp = "b1db4737-0e75-4152-8987-ba192605c3da",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0


            });

        _builder.Entity<User>()
            .HasData(new User
            {
                Id = 2,
                UserName = "seller",
                NormalizedUserName = "SELLER",
                Email = "seller@vm.com",
                NormalizedEmail = "SELLER@VM.COM",
                EmailConfirmed = false,
                Deposit = null,
                PasswordHash = "AQAAAAIAAYagAAAAEMpf+HOcOP1h3YPp6oapYPAhSZylSZiPGjAOqATnWJjj2+Nncqc2rXS/IBn4gSyB0Q==",
                SecurityStamp = "MPLTHNGG7JU3YGBA5O6I7LBFRXEBP6N6",
                ConcurrencyStamp = "b1db4737-0e75-4152-8987-ba192605c3da",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0


            });
        _builder.Entity<User>().HasData(new User
        {
            Id = 3,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@vm.com",
            NormalizedEmail = "ADMIN@VM.COM",
            EmailConfirmed = false,
            Deposit = null,
            PasswordHash = "AQAAAAIAAYagAAAAEMpf+HOcOP1h3YPp6oapYPAhSZylSZiPGjAOqATnWJjj2+Nncqc2rXS/IBn4gSyB0Q==",
            SecurityStamp = "MPLTHNGG7JU3YGBA5O6I7LBFRXEBP6N6",
            ConcurrencyStamp = "b1db4737-0e75-4152-8987-ba192605c3da",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        });
        _builder.Entity<IdentityUserRole<int>>()
            .HasData(new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 3
            });
        _builder.Entity<IdentityUserRole<int>>()
         .HasData(new IdentityUserRole<int>
         {
             UserId = 2,
             RoleId = 2
         });
        _builder.Entity<IdentityUserRole<int>>()
      .HasData(new IdentityUserRole<int>
      {
          UserId = 3,
          RoleId = 1
      });
        _builder.Entity<Product>().HasData(
    new Product
    {
        Id = 1,
        ProductName = "Soda",
        AmountAvailable = 10,
        Cost = 50,
        SellerId = 1
    },
    new Product
    {
        Id = 2,
        ProductName = "Chips",
        AmountAvailable = 15,
        Cost = 100,
        SellerId = 1
    },
    new Product
    {
        Id = 3,
        ProductName = "Candy Bar",
        AmountAvailable = 20,
        Cost = 50,
        SellerId = 1
    },
    new Product
    {
        Id = 4,
        ProductName = "Bottled Water",
        AmountAvailable = 12,
        Cost = 100,
        SellerId = 1
    },
    new Product
    {
        Id = 5,
        ProductName = "Juice",
        AmountAvailable = 8,
        Cost = 20,
        SellerId = 1
    },
    new Product
    {
        Id = 6,
        ProductName = "Energy Drink",
        AmountAvailable = 25,
        Cost = 5,
        SellerId = 1
    },
    new Product
    {
        Id = 7,
        ProductName = "Granola Bar",
        AmountAvailable = 18,
        Cost = 50,
        SellerId = 1
    },
    new Product
    {
        Id = 8,
        ProductName = "Cookies",
        AmountAvailable = 30,
        Cost = 100,
        SellerId = 1
    },
    new Product
    {
        Id = 9,
        ProductName = "Fruit Snack",
        AmountAvailable = 22,
        Cost = 10,
        SellerId = 1
    },
    new Product
    {
        Id = 10,
        ProductName = "Popcorn",
        AmountAvailable = 17,
        Cost = 50,
        SellerId = 1
    }
);


    }

}
