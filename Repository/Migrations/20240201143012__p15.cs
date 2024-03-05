using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollbackpre_Prerrequisito_PrerrequisitoID",
                table: "Rollbackpre");

            migrationBuilder.DropTable(
                name: "Prerrequisito");

            migrationBuilder.AlterColumn<int>(
                name: "PrerrequisitoID",
                table: "Rollbackpre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PrerequisiteID",
                table: "Rollbackpre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Prerequisite",
                columns: table => new
                {
                    PrerequisiteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DataStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false),
                    ResponsibleAreaID = table.Column<int>(type: "int", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisite", x => x.PrerequisiteID);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Change_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Change_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisite_ResponsibleArea_ResponsibleAreaID",
                        column: x => x.ResponsibleAreaID,
                        principalTable: "ResponsibleArea",
                        principalColumn: "ResponsibleAreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisite_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_ChangeID",
                table: "Prerequisite",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_ChangesChangeID",
                table: "Prerequisite",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_ResponsibleAreaID",
                table: "Prerequisite",
                column: "ResponsibleAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_UserID",
                table: "Prerequisite",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollbackpre_Prerequisite_PrerrequisitoID",
                table: "Rollbackpre",
                column: "PrerrequisitoID",
                principalTable: "Prerequisite",
                principalColumn: "PrerequisiteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollbackpre_Prerequisite_PrerrequisitoID",
                table: "Rollbackpre");

            migrationBuilder.DropTable(
                name: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "PrerequisiteID",
                table: "Rollbackpre");

            migrationBuilder.AlterColumn<int>(
                name: "PrerrequisitoID",
                table: "Rollbackpre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Prerrequisito",
                columns: table => new
                {
                    PrerrequisitoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true),
                    DataEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false),
                    ResponsibleAreaID = table.Column<int>(type: "int", maxLength: 5000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerrequisito", x => x.PrerrequisitoID);
                    table.ForeignKey(
                        name: "FK_Prerrequisito_Change_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisito_Change_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Change",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisito_ResponsibleArea_ResponsibleAreaID",
                        column: x => x.ResponsibleAreaID,
                        principalTable: "ResponsibleArea",
                        principalColumn: "ResponsibleAreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisito_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisito_ChangeID",
                table: "Prerrequisito",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisito_ChangesChangeID",
                table: "Prerrequisito",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisito_ResponsibleAreaID",
                table: "Prerrequisito",
                column: "ResponsibleAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisito_UserID",
                table: "Prerrequisito",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollbackpre_Prerrequisito_PrerrequisitoID",
                table: "Rollbackpre",
                column: "PrerrequisitoID",
                principalTable: "Prerrequisito",
                principalColumn: "PrerrequisitoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
