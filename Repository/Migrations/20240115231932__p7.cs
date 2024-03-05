using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicatives_Changes_ChangeID",
                table: "ChangeAplicatives");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_RollbackPlans_RollbackPlanID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisitos_Rollbackpres_RollbackPreID",
                table: "Prerrequisitos");

            migrationBuilder.DropIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpres");

            migrationBuilder.DropIndex(
                name: "IX_Prerrequisitos_RollbackPreID",
                table: "Prerrequisitos");

            migrationBuilder.DropIndex(
                name: "IX_Plans_RollbackPlanID",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAplicatives_ChangeID",
                table: "ChangeAplicatives");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Contacts",
                newName: "UserID");

            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    RequestTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.RequestTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TypologyChanges",
                columns: table => new
                {
                    TypologyChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypologyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypologyChanges", x => x.TypologyChangeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpres",
                column: "PrerrequisitoID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "TypologyChanges");

            migrationBuilder.DropIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpres");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Contacts",
                newName: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpres",
                column: "PrerrequisitoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_RollbackPreID",
                table: "Prerrequisitos",
                column: "RollbackPreID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_RollbackPlanID",
                table: "Plans",
                column: "RollbackPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicatives_ChangeID",
                table: "ChangeAplicatives",
                column: "ChangeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAplicatives_Changes_ChangeID",
                table: "ChangeAplicatives",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_RollbackPlans_RollbackPlanID",
                table: "Plans",
                column: "RollbackPlanID",
                principalTable: "RollbackPlans",
                principalColumn: "RollbackPlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisitos_Rollbackpres_RollbackPreID",
                table: "Prerrequisitos",
                column: "RollbackPreID",
                principalTable: "Rollbackpres",
                principalColumn: "RollbackPreID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
