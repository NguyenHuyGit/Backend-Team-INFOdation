using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.BackendAPI.Migrations
{
    public partial class NewDbRemote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 338, DateTimeKind.Local).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 339, DateTimeKind.Local).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 339, DateTimeKind.Local).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 339, DateTimeKind.Local).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 339, DateTimeKind.Local).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 10, 41, 15, 339, DateTimeKind.Local).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "488faf30-a6dd-44b5-9203-3337d1edfabb", "AQAAAAEAACcQAAAAEOpkc8sjK141w8lMXUNtMoXcFzulNKk8G+qpeupMwvNDFNMV3rkQWgyOZ06XzER54A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cdf5c8fb-b7c0-455c-8134-94ef0cf92717"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c91acb14-5a19-4ef8-9577-9ca341205f25", "AQAAAAEAACcQAAAAEDzz44ppugfatzjV/gjAJegStslkkLSnklGYMVyuXrgL8hyfG92OK+amGg0FiRwNwQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 599, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 600, DateTimeKind.Local).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 600, DateTimeKind.Local).AddTicks(9097));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 600, DateTimeKind.Local).AddTicks(9100));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 600, DateTimeKind.Local).AddTicks(9102));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2022, 4, 27, 8, 59, 51, 600, DateTimeKind.Local).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b0179cb-db15-48f5-a093-3fa18f86b82f", "AQAAAAEAACcQAAAAEOdUKqJV0A5lKqR9VP4A22Kdq/kNkyEFBFal/OYloE7w1GkI2H3jnNAvXHZjGI5xWg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cdf5c8fb-b7c0-455c-8134-94ef0cf92717"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7eb32fc9-36cb-46f9-b264-f274472af201", "AQAAAAEAACcQAAAAEPDHOPVPGyYg6D81859A+qmET1+r1bql76bfMvgv4IwOIi+kh5+LKbfW/XUYELWblA==" });
        }
    }
}
