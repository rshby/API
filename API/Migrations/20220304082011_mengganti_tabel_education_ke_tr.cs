using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class mengganti_tabel_education_ke_tr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_education_tb_m_university_University_Id",
                table: "tb_m_education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_Education_Id",
                table: "tb_tr_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_education",
                table: "tb_m_education");

            migrationBuilder.RenameTable(
                name: "tb_m_education",
                newName: "tb_tr_education");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_education_University_Id",
                table: "tb_tr_education",
                newName: "IX_tb_tr_education_University_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_education",
                table: "tb_tr_education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_education_tb_m_university_University_Id",
                table: "tb_tr_education",
                column: "University_Id",
                principalTable: "tb_m_university",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_tr_education_Education_Id",
                table: "tb_tr_profiling",
                column: "Education_Id",
                principalTable: "tb_tr_education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_education_tb_m_university_University_Id",
                table: "tb_tr_education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_tr_education_Education_Id",
                table: "tb_tr_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_education",
                table: "tb_tr_education");

            migrationBuilder.RenameTable(
                name: "tb_tr_education",
                newName: "tb_m_education");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_education_University_Id",
                table: "tb_m_education",
                newName: "IX_tb_m_education_University_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_education",
                table: "tb_m_education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_education_tb_m_university_University_Id",
                table: "tb_m_education",
                column: "University_Id",
                principalTable: "tb_m_university",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_Education_Id",
                table: "tb_tr_profiling",
                column: "Education_Id",
                principalTable: "tb_m_education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
