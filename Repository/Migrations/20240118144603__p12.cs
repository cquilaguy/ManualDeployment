using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blueprint_Change_ChangeID",
                table: "Blueprint");

            migrationBuilder.DropIndex(
                name: "IX_Blueprint_ChangeID",
                table: "Blueprint");

            migrationBuilder.DropColumn(
                name: "ChangeID",
                table: "Blueprint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeID",
                table: "Blueprint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blueprint_ChangeID",
                table: "Blueprint",
                column: "ChangeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blueprint_Change_ChangeID",
                table: "Blueprint",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
