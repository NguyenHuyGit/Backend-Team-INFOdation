using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.BackendAPI.Migrations
{
    public partial class FinalDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 0),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    UserCreate = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UserUpdate = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "DELL" },
                    { 2, "ASUS" },
                    { 3, "ACER" },
                    { 4, "HP" },
                    { 5, "SAMSUNG" },
                    { 6, "APPLE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "3686be03-7a0d-4895-b316-7dbba283b45d", "backendteam@gmail.com", true, "Hung", "Nguyen", false, null, "backendteam@gmail.com", "admin", "AQAAAAEAACcQAAAAEO2hdo1R12uTOl+S+wcCW1FmoSeh7jSGvvi2f+ERkoHDAmrLivv0SSfegKcMrHhepw==", null, false, "", false, "admin" },
                    { new Guid("cdf5c8fb-b7c0-455c-8134-94ef0cf92717"), 0, "e68d44e7-ffb1-4b40-8a95-88aba30362e1", "backend@gmail.com", true, "Liem", "Nguyen", false, null, "backend@gmail.com", "liemnv", "AQAAAAEAACcQAAAAEMmhtvLeFf4LAPw5HB70K/LApkaBajsUxCrUCKuBzrSg0OxPXcD4Al1CGwp7klvunQ==", null, false, "", false, "liemnv" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Phụ kiện Laptop" },
                    { 4, 1, "Máy tính" },
                    { 11, 1, "Máy in" },
                    { 9, 2, "Laptop gaming" },
                    { 8, 3, "Laptop gaming" },
                    { 10, 4, "Máy tính" },
                    { 5, 5, "Điện thoại" },
                    { 2, 6, "Phụ kiện Ipad" },
                    { 3, 6, "Ipad" },
                    { 7, 6, "Iphone" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreateDate", "Description", "Name", "Quantity", "UpdateDate", "UserCreate", "UserUpdate" },
                values: new object[,]
                {
                    { 3, 1, new DateTime(2022, 4, 25, 19, 48, 9, 863, DateTimeKind.Local).AddTicks(1203), "Sạc 100W", "Củ sạc laptop M01", 10, null, "Liêm", null },
                    { 1, 4, new DateTime(2022, 4, 25, 19, 48, 9, 862, DateTimeKind.Local).AddTicks(2179), "Laptop văn phòng", "Vostro 3578", 3, null, "Liêm", null },
                    { 6, 4, new DateTime(2022, 4, 25, 19, 48, 9, 863, DateTimeKind.Local).AddTicks(1210), "Laptop văn phòng mạnh mẽ", "Vostro 7799", 2, null, "Liêm", null },
                    { 5, 9, new DateTime(2022, 4, 25, 19, 48, 9, 863, DateTimeKind.Local).AddTicks(1208), "Laptop gaming mạnh mẽ", "TUF Gaming 22KW", 2, null, "Liêm", null },
                    { 2, 5, new DateTime(2022, 4, 25, 19, 48, 9, 863, DateTimeKind.Local).AddTicks(1168), "Điện thoại thời thượng", "Galaxy A52s", 5, null, "Liêm", null },
                    { 4, 7, new DateTime(2022, 4, 25, 19, 48, 9, 863, DateTimeKind.Local).AddTicks(1206), "Điện thoại cao cấp", "Iphone 13 Pro Max", 5, null, "Liêm", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BrandId",
                table: "Categories",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
