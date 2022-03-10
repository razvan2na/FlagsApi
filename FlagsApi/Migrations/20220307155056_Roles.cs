using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlagsApi.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e465888f-be57-4544-98f8-4367308dbb5c", "f4bec5fd-fe6e-4432-ab0f-8c279a246566", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e465888f-be57-4544-98f8-4367308dbb5c");
        }
    }
}
