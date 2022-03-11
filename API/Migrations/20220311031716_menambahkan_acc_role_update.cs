using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class menambahkan_acc_role_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_RoleId",
                table: "tb_tr_accountrole");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_tr_account_AccountNIK",
                table: "tb_tr_accountrole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountrole_AccountNIK",
                table: "tb_tr_accountrole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountrole_RoleId",
                table: "tb_tr_accountrole");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "tb_tr_accountrole");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "tb_tr_accountrole");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_tr_accountrole",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_NIK",
                table: "tb_tr_accountrole",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_Role_Id",
                table: "tb_tr_accountrole",
                column: "Role_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_Role_Id",
                table: "tb_tr_accountrole",
                column: "Role_Id",
                principalTable: "tb_m_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_tr_account_NIK",
                table: "tb_tr_accountrole",
                column: "NIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_Role_Id",
                table: "tb_tr_accountrole");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_tr_account_NIK",
                table: "tb_tr_accountrole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountrole_NIK",
                table: "tb_tr_accountrole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountrole_Role_Id",
                table: "tb_tr_accountrole");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_tr_accountrole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "tb_tr_accountrole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "tb_tr_accountrole",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_AccountNIK",
                table: "tb_tr_accountrole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_RoleId",
                table: "tb_tr_accountrole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_RoleId",
                table: "tb_tr_accountrole",
                column: "RoleId",
                principalTable: "tb_m_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_tr_account_AccountNIK",
                table: "tb_tr_accountrole",
                column: "AccountNIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
