using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicatives",
                columns: table => new
                {
                    ApplicativeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameApplicative = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicatives", x => x.ApplicativeID);
                });

            migrationBuilder.CreateTable(
                name: "Environments",
                columns: table => new
                {
                    EnvironmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEnvironment = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environments", x => x.EnvironmentID);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeofProfile = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileID);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibleAreas",
                columns: table => new
                {
                    ResponsibleAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Responsible = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleAreas", x => x.ResponsibleAreaId);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    ServerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameServer = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.ServerID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetworkUser = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentApplicatives",
                columns: table => new
                {
                    EnvironmentApplicativeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicativeID = table.Column<int>(type: "int", nullable: false),
                    ServerID = table.Column<int>(type: "int", nullable: false),
                    EnvironmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentApplicatives", x => x.EnvironmentApplicativeID);
                    table.ForeignKey(
                        name: "FK_EnvironmentApplicatives_Applicatives_ApplicativeID",
                        column: x => x.ApplicativeID,
                        principalTable: "Applicatives",
                        principalColumn: "ApplicativeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnvironmentApplicatives_Environments_EnvironmentID",
                        column: x => x.EnvironmentID,
                        principalTable: "Environments",
                        principalColumn: "EnvironmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnvironmentApplicatives_Servers_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Servers",
                        principalColumn: "ServerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileUsers",
                columns: table => new
                {
                    ProfileUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsers", x => x.ProfileUserID);
                    table.ForeignKey(
                        name: "FK_ProfileUsers_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeAplicatives",
                columns: table => new
                {
                    ChangeApplicativeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicativeID = table.Column<int>(type: "int", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    EnvironmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeAplicatives", x => x.ChangeApplicativeID);
                    table.ForeignKey(
                        name: "FK_ChangeAplicatives_Applicatives_ApplicativeID",
                        column: x => x.ApplicativeID,
                        principalTable: "Applicatives",
                        principalColumn: "ApplicativeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAplicatives_Environments_EnvironmentID",
                        column: x => x.EnvironmentID,
                        principalTable: "Environments",
                        principalColumn: "EnvironmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Changes",
                columns: table => new
                {
                    ChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    changeDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    requestType = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    changerNumber = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    checkList = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    applicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deploymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    changeType = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChangeApplicativeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changes", x => x.ChangeID);
                    table.ForeignKey(
                        name: "FK_Changes_ChangeAplicatives_ChangeApplicativeID",
                        column: x => x.ChangeApplicativeID,
                        principalTable: "ChangeAplicatives",
                        principalColumn: "ChangeApplicativeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Changes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blueprints",
                columns: table => new
                {
                    BlueprintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blueprints", x => x.BlueprintID);
                    table.ForeignKey(
                        name: "FK_Blueprints_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observations = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalUsers",
                columns: table => new
                {
                    FunctionalUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    DataStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalUsers", x => x.FunctionalUserID);
                    table.ForeignKey(
                        name: "FK_FunctionalUsers_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionalUsers_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionalUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Postimplantacions",
                columns: table => new
                {
                    PostimplantacionID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Postimplantacions", x => x.PostimplantacionID);
                    table.ForeignKey(
                        name: "FK_Postimplantacions_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantacions_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postimplantacions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Approved = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Reprobate = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Error = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_Results_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Signatures",
                columns: table => new
                {
                    SignatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observatins = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    EmailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TrainedUserID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatures", x => x.SignatureID);
                    table.ForeignKey(
                        name: "FK_Signatures_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Signatures_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Signatures_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    TrainingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comments = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DataTraining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Issues = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.TrainingID);
                    table.ForeignKey(
                        name: "FK_Trainings_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserChanges",
                columns: table => new
                {
                    UserChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChanges", x => x.UserChangeID);
                    table.ForeignKey(
                        name: "FK_UserChanges_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserChanges_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    DataStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    supplierArea = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    RollbackPlanID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanID);
                    table.ForeignKey(
                        name: "FK_Plans_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plans_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RollbackPlans",
                columns: table => new
                {
                    RollbackPlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollbackPlans", x => x.RollbackPlanID);
                    table.ForeignKey(
                        name: "FK_RollbackPlans_Plans_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Plans",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerrequisitos",
                columns: table => new
                {
                    PrerrequisitoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DataStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false),
                    SupplierArea = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RollbackPreID = table.Column<int>(type: "int", nullable: false),
                    ChangesChangeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerrequisitos", x => x.PrerrequisitoID);
                    table.ForeignKey(
                        name: "FK_Prerrequisitos_Changes_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisitos_Changes_ChangesChangeID",
                        column: x => x.ChangesChangeID,
                        principalTable: "Changes",
                        principalColumn: "ChangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisitos_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rollbackpres",
                columns: table => new
                {
                    RollbackPreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    PrerrequisitoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollbackpres", x => x.RollbackPreID);
                    table.ForeignKey(
                        name: "FK_Rollbackpres_Prerrequisitos_PrerrequisitoID",
                        column: x => x.PrerrequisitoID,
                        principalTable: "Prerrequisitos",
                        principalColumn: "PrerrequisitoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_ChangeID",
                table: "Blueprints",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicatives_ApplicativeID",
                table: "ChangeAplicatives",
                column: "ApplicativeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicatives_ChangeID",
                table: "ChangeAplicatives",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicatives_EnvironmentID",
                table: "ChangeAplicatives",
                column: "EnvironmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ChangeApplicativeID",
                table: "Changes",
                column: "ChangeApplicativeID");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_UserID",
                table: "Changes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ChangeID",
                table: "Contacts",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ChangesChangeID",
                table: "Contacts",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentApplicatives_ApplicativeID",
                table: "EnvironmentApplicatives",
                column: "ApplicativeID");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentApplicatives_EnvironmentID",
                table: "EnvironmentApplicatives",
                column: "EnvironmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentApplicatives_ServerID",
                table: "EnvironmentApplicatives",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalUsers_ChangeID",
                table: "FunctionalUsers",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalUsers_ChangesChangeID",
                table: "FunctionalUsers",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalUsers_UserID",
                table: "FunctionalUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ChangeID",
                table: "Plans",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ChangesChangeID",
                table: "Plans",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_RollbackPlanID",
                table: "Plans",
                column: "RollbackPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacions_ChangeID",
                table: "Postimplantacions",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacions_ChangesChangeID",
                table: "Postimplantacions",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Postimplantacions_UserID",
                table: "Postimplantacions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_ChangeID",
                table: "Prerrequisitos",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_ChangesChangeID",
                table: "Prerrequisitos",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_RollbackPreID",
                table: "Prerrequisitos",
                column: "RollbackPreID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_UserID",
                table: "Prerrequisitos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_ProfileID",
                table: "ProfileUsers",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_UserID",
                table: "ProfileUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ChangeID",
                table: "Results",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ChangesChangeID",
                table: "Results",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_RollbackPlans_PlanID",
                table: "RollbackPlans",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpres",
                column: "PrerrequisitoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_ChangeID",
                table: "Signatures",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_ChangesChangeID",
                table: "Signatures",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_UserID",
                table: "Signatures",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ChangeID",
                table: "Trainings",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ChangesChangeID",
                table: "Trainings",
                column: "ChangesChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_UserID",
                table: "Trainings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserChanges_ChangeID",
                table: "UserChanges",
                column: "ChangeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserChanges_UserID",
                table: "UserChanges",
                column: "UserID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicatives_Changes_ChangeID",
                table: "ChangeAplicatives");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Changes_ChangeID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Changes_ChangesChangeID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisitos_Changes_ChangeID",
                table: "Prerrequisitos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisitos_Changes_ChangesChangeID",
                table: "Prerrequisitos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisitos_Users_UserID",
                table: "Prerrequisitos");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_RollbackPlans_RollbackPlanID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisitos_Rollbackpres_RollbackPreID",
                table: "Prerrequisitos");

            migrationBuilder.DropTable(
                name: "Blueprints");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "EnvironmentApplicatives");

            migrationBuilder.DropTable(
                name: "FunctionalUsers");

            migrationBuilder.DropTable(
                name: "Postimplantacions");

            migrationBuilder.DropTable(
                name: "ProfileUsers");

            migrationBuilder.DropTable(
                name: "ResponsibleAreas");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Signatures");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "UserChanges");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Changes");

            migrationBuilder.DropTable(
                name: "ChangeAplicatives");

            migrationBuilder.DropTable(
                name: "Applicatives");

            migrationBuilder.DropTable(
                name: "Environments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RollbackPlans");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Rollbackpres");

            migrationBuilder.DropTable(
                name: "Prerrequisitos");
        }
    }
}
