using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollbackpre_Prerequisite_PrerrequisitoID",
                table: "Rollbackpre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rollbackpre",
                table: "Rollbackpre");

            migrationBuilder.RenameTable(
                name: "Rollbackpre",
                newName: "RollbackPre");

            migrationBuilder.RenameIndex(
                name: "IX_Rollbackpre_PrerrequisitoID",
                table: "RollbackPre",
                newName: "IX_RollbackPre_PrerrequisitoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollbackPre",
                table: "RollbackPre",
                column: "RollbackPreID");

            migrationBuilder.AddForeignKey(
                name: "FK_RollbackPre_Prerequisite_PrerrequisitoID",
                table: "RollbackPre",
                column: "PrerrequisitoID",
                principalTable: "Prerequisite",
                principalColumn: "PrerequisiteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollbackPre_Prerequisite_PrerrequisitoID",
                table: "RollbackPre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollbackPre",
                table: "RollbackPre");

            migrationBuilder.RenameTable(
                name: "RollbackPre",
                newName: "Rollbackpre");

            migrationBuilder.RenameIndex(
                name: "IX_RollbackPre_PrerrequisitoID",
                table: "Rollbackpre",
                newName: "IX_Rollbackpre_PrerrequisitoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rollbackpre",
                table: "Rollbackpre",
                column: "RollbackPreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollbackpre_Prerequisite_PrerrequisitoID",
                table: "Rollbackpre",
                column: "PrerrequisitoID",
                principalTable: "Prerequisite",
                principalColumn: "PrerequisiteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
