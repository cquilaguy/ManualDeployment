using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class p_19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Change",
                newName: "StatusID");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Change_StatusID",
                table: "Change",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Change_Status_StatusID",
                table: "Change",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Change_Status_StatusID",
                table: "Change");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Change_StatusID",
                table: "Change");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "Change",
                newName: "State");
        }
    }
}
