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
                new Brand() { Id = 3, Name = "Panasonic" },
                new Brand() { Id = 4, Name = "Samsung" },
                new Brand() { Id = 5, Name = "Apple" }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Laptop", BrandId = 1 },
                new Category() { Id = 2, Name = "Máy tính bàn", BrandId = 1 },
                new Category() { Id = 3, Name = "Tủ lạnh", BrandId = 3 },
                new Category() { Id = 4, Name = "Điện thoại", BrandId = 4 },
                new Category() { Id = 5, Name = "Điện thoại", BrandId = 5 }
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
                    CategoryId = 1,
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
                    CategoryId = 4,
                },
                new Product()
                {
                    Id = 3,
                    Name = "Tủ lạnh PN123",
                    Quantity = 10,
                    Description = "Tủ lạnh hiện đại",
                    Status = 0,
                    UserCreate = "Liêm",
                    CreateDate = System.DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 3,
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
                    CategoryId = 5,
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
                    CategoryId = 1,
                }
                );
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "backendteam@gmail.com",
                NormalizedEmail = "backendteam@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Liem",
                LastName = "Nguyen",
            });
        }
    }
}
