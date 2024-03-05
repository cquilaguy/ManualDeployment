using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postimplantacion");

            migrationBuilder.CreateTable(
                name: "Postimplantation",
                columns: table => new
                {
                    PostimplantationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DataStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postimplantation", x => x.PostimplantationID);
                    table.ForeignKey(
                        name: "FK_Postimplantation_Change_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantation_Change_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantation_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantation_ChangeID",
                table: "Postimplantation",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantation_ChangesChangeID",
                table: "Postimplantation",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantation_UserID",
                table: "Postimplantation",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postimplantation");

            migrationBuilder.CreateTable(
                name: "Postimplantacion",
                columns: table => new
                {
                    PostimplantacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true),
                    DataEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postimplantacion", x => x.PostimplantacionID);
                    table.ForeignKey(
                        name: "FK_Postimplantacion_Change_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantacion_Change_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantacion_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacion_ChangeID",
                table: "Postimplantacion",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacion_ChangesChangeID",
                table: "Postimplantacion",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacion_UserID",
                table: "Postimplantacion",
                column: "UserID");
        }
    }
}
