using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class menambahkan_acc_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_accountrole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_accountrole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_accountrole_tb_m_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_tr_accountrole_tb_tr_account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_tr_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_AccountNIK",
                table: "tb_tr_accountrole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_RoleId",
                table: "tb_tr_accountrole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_accountrole");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}
