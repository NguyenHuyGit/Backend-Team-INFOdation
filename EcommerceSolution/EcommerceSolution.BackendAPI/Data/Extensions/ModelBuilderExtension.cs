using EcommerceSolution.BackendAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceSolution.BackendAPI.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand() { Id = 1, Name = "DELL" },
                new Brand() { Id = 2, Name = "ASUS" },
                new Brand() { Id = 3, Name = "ACER" },
                new Brand() { Id = 4, Name = "HP" },
                new Brand() { Id = 5, Name = "SAMSUNG" },
                new Brand() { Id = 6, Name = "APPLE" }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Phụ kiện Laptop", BrandId = 1 },//Dell-1
                new Category() { Id = 2, Name = "Phụ kiện Ipad", BrandId = 6 },//Apple-6
                new Category() { Id = 3, Name = "Ipad", BrandId = 6 },//Apple-6
                new Category() { Id = 4, Name = "Máy tính", BrandId = 1 },//Dell-1
                new Category() { Id = 5, Name = "Điện thoại", BrandId = 5 },//Samsung-5
                new Category() { Id = 7, Name = "Iphone", BrandId = 6 },//Apple-6
                new Category() { Id = 8, Name = "Laptop gaming", BrandId = 3 },//Acer-3
                new Category() { Id = 9, Name = "Laptop gaming", BrandId = 2 },//Asus-2
                new Category() { Id = 10, Name = "Máy tính", BrandId = 4 },//HP-4
                new Category() { Id = 11, Name = "Máy in", BrandId = 1 }//Dell-1
                );
            modelBuilder.Entity<Product>().HasData(
                new Product() { 
                    Id = 1, 
                    Name = "Vostro 3578", 
                    Quantity = 3,
                    Description = "Laptop văn phòng",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 4,
                    BrandId = 1,
                },
                new Product()
                {
                    Id = 2,
                    Name = "Galaxy A52s",
                    Quantity = 5,
                    Description = "Điện thoại thời thượng",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 5,
                    BrandId = 5,
                },
                new Product()
                {
                    Id = 3,
                    Name = "Củ sạc laptop M01",
                    Quantity = 10,
                    Description = "Sạc 100W",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 1,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 4,
                    Name = "Iphone 13 Pro Max",
                    Quantity = 5,
                    Description = "Điện thoại cao cấp",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 7,
                    BrandId = 6
                },
                new Product()
                {
                    Id = 5,
                    Name = "TUF Gaming 22KW",
                    Quantity = 2,
                    Description = "Laptop gaming mạnh mẽ",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 9,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 6,
                    Name = "Vostro 7799",
                    Quantity = 2,
                    Description = "Laptop văn phòng mạnh mẽ",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 4,
                    BrandId = 1
                }
                );

            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var userId = new Guid("CDF5C8FB-B7C0-455C-8134-94EF0CF92717");
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "backendteam@gmail.com",
                    NormalizedEmail = "backendteam@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Hung",
                    LastName = "Nguyen",
                },
                new User
                {
                    Id = userId,
                    UserName = "liemnv",
                    NormalizedUserName = "liemnv",
                    Email = "backend@gmail.com",
                    NormalizedEmail = "backend@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Liem",
                    LastName = "Nguyen",
                }
           );
        }
    }
}
