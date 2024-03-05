using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Server_ServerID",
                table: "Environment");

            migrationBuilder.DropIndex(
                name: "IX_Environment_ServerID",
                table: "Environment");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "Environment");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "Blueprint");

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentID",
                table: "Server",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Change",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApplicativeID",
                table: "Blueprint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Server_EnvironmentID",
                table: "Server",
                column: "EnvironmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Blueprint_ApplicativeID",
                table: "Blueprint",
                column: "ApplicativeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blueprint_Applicative_ApplicativeID",
                table: "Blueprint",
                column: "ApplicativeID",
                principalTable: "Applicative",
                principalColumn: "ApplicativeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Environment_EnvironmentID",
                table: "Server",
                column: "EnvironmentID",
                principalTable: "Environment",
                principalColumn: "EnvironmentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blueprint_Applicative_ApplicativeID",
                table: "Blueprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_Environment_EnvironmentID",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Server_EnvironmentID",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Blueprint_ApplicativeID",
                table: "Blueprint");

            migrationBuilder.DropColumn(
                name: "EnvironmentID",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Change");

            migrationBuilder.DropColumn(
                name: "ApplicativeID",
                table: "Blueprint");

            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "Environment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Blueprint",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Environment_ServerID",
                table: "Environment",
                column: "ServerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Server_ServerID",
                table: "Environment",
                column: "ServerID",
                principalTable: "Server",
                principalColumn: "ServerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
