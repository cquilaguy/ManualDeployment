using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class p_22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicative_Applicative_ApplicativeID",
                table: "ChangeAplicative");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicative_Change_ChangeID",
                table: "ChangeAplicative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeAplicative",
                table: "ChangeAplicative");

            migrationBuilder.RenameTable(
                name: "ChangeAplicative",
                newName: "ChangeApplicative");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAplicative_ChangeID",
                table: "ChangeApplicative",
                newName: "IX_ChangeApplicative_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAplicative_ApplicativeID",
                table: "ChangeApplicative",
                newName: "IX_ChangeApplicative_ApplicativeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeApplicative",
                table: "ChangeApplicative",
                column: "ChangeApplicativeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeApplicative_Applicative_ApplicativeID",
                table: "ChangeApplicative",
                column: "ApplicativeID",
                principalTable: "Applicative",
                principalColumn: "ApplicativeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeApplicative_Change_ChangeID",
                table: "ChangeApplicative",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeApplicative_Applicative_ApplicativeID",
                table: "ChangeApplicative");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeApplicative_Change_ChangeID",
                table: "ChangeApplicative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeApplicative",
                table: "ChangeApplicative");

            migrationBuilder.RenameTable(
                name: "ChangeApplicative",
                newName: "ChangeAplicative");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeApplicative_ChangeID",
                table: "ChangeAplicative",
                newName: "IX_ChangeAplicative_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeApplicative_ApplicativeID",
                table: "ChangeAplicative",
                newName: "IX_ChangeAplicative_ApplicativeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeAplicative",
                table: "ChangeAplicative",
                column: "ChangeApplicativeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAplicative_Applicative_ApplicativeID",
                table: "ChangeAplicative",
                column: "ApplicativeID",
                principalTable: "Applicative",
                principalColumn: "ApplicativeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAplicative_Change_ChangeID",
                table: "ChangeAplicative",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
