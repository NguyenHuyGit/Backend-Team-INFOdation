using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceSolution.BackendAPI.Migrations
{
    public partial class TestDbInEnv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 24, 21, 1, 44, 802, DateTimeKind.Local).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2022, 4, 24, 21, 1, 44, 803, DateTimeKind.Local).AddTicks(6294));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2022, 4, 24, 21, 1, 44, 803, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2022, 4, 24, 21, 1, 44, 803, DateTimeKind.Local).AddTicks(6341));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2022, 4, 24, 21, 1, 44, 803, DateTimeKind.Local).AddTicks(6344));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ff028e5-d89d-4b68-acc5-23653906e88b", "AQAAAAEAACcQAAAAECXUD6iypG7u7oVeJid1u3IDx1x9oqc/uhwzB5VE8eA5DMqi+AGkz80clVTr17r54Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 23, 16, 15, 27, 147, DateTimeKind.Local).AddTicks(5969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2022, 4, 23, 16, 15, 27, 148, DateTimeKind.Local).AddTicks(4843));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2022, 4, 23, 16, 15, 27, 148, DateTimeKind.Local).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2022, 4, 23, 16, 15, 27, 148, DateTimeKind.Local).AddTicks(4893));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2022, 4, 23, 16, 15, 27, 148, DateTimeKind.Local).AddTicks(4895));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f80d850b-c734-4e95-b681-2864270070c9", "AQAAAAEAACcQAAAAEM8vzeMyrScAi+os2F/GZwayaU4XqNHzTHp+IRVnDEg/0volOANOVuOzb8bkdcfG1g==" });
        }
    }
}
