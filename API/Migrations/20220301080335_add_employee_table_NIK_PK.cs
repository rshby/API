using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_employee_table_NIK_PK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "Employees",
                newName: "id");
        }
    }
}
