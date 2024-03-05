using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Change_TypologyChange_TypologyChangeID",
                table: "Change");

            migrationBuilder.DropTable(
                name: "TypologyChange");

            migrationBuilder.DropTable(
                name: "UserChange");

            migrationBuilder.RenameColumn(
                name: "TypologyChangeID",
                table: "Change",
                newName: "TypologyID");

            migrationBuilder.RenameIndex(
                name: "IX_Change_TypologyChangeID",
                table: "Change",
                newName: "IX_Change_TypologyID");

            migrationBuilder.CreateTable(
                name: "Typology",
                columns: table => new
                {
                    TypologyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypologyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typology", x => x.TypologyID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Change_Typology_TypologyID",
                table: "Change",
                column: "TypologyID",
                principalTable: "Typology",
                principalColumn: "TypologyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Change_Typology_TypologyID",
                table: "Change");

            migrationBuilder.DropTable(
                name: "Typology");

            migrationBuilder.RenameColumn(
                name: "TypologyID",
                table: "Change",
                newName: "TypologyChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Change_TypologyID",
                table: "Change",
                newName: "IX_Change_TypologyChangeID");

            migrationBuilder.CreateTable(
                name: "TypologyChange",
                columns: table => new
                {
                    TypologyChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypologyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypologyChange", x => x.TypologyChangeID);
                });

            migrationBuilder.CreateTable(
                name: "UserChange",
                columns: table => new
                {
                    UserChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChange", x => x.UserChangeID);
                    table.ForeignKey(
                        name: "FK_UserChange_Change_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserChange_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChange_ChangeID",
                table: "UserChange",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserChange_UserID",
                table: "UserChange",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Change_TypologyChange_TypologyChangeID",
                table: "Change",
                column: "TypologyChangeID",
                principalTable: "TypologyChange",
                principalColumn: "TypologyChangeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
