using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _p10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blueprints_Changes_ChangeID",
                table: "Blueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicatives_Applicatives_ApplicativeID",
                table: "ChangeAplicatives");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicatives_Environments_EnvironmentID",
                table: "ChangeAplicatives");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ChangeAplicatives_ChangeApplicativeID",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_Users_UserID",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Changes_ChangeID",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Changes_ChangesChangeID",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUsers_Changes_ChangeID",
                table: "FunctionalUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUsers_Changes_ChangesChangeID",
                table: "FunctionalUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUsers_Users_UserID",
                table: "FunctionalUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Changes_ChangeID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Changes_ChangesChangeID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacions_Changes_ChangeID",
                table: "Postimplantacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacions_Changes_ChangesChangeID",
                table: "Postimplantacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacions_Users_UserID",
                table: "Postimplantacions");

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
                name: "FK_ProfileUsers_Profiles_ProfileID",
                table: "ProfileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUsers_Users_UserID",
                table: "ProfileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Changes_ChangeID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Changes_ChangesChangeID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_RollbackPlans_Plans_PlanID",
                table: "RollbackPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Rollbackpres_Prerrequisitos_PrerrequisitoID",
                table: "Rollbackpres");

            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_Changes_ChangeID",
                table: "Signatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_Changes_ChangesChangeID",
                table: "Signatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_Users_UserID",
                table: "Signatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Changes_ChangeID",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Changes_ChangesChangeID",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Users_UserID",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChanges_Changes_ChangeID",
                table: "UserChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChanges_Users_UserID",
                table: "UserChanges");

            migrationBuilder.DropTable(
                name: "EnvironmentApplicatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChanges",
                table: "UserChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyChanges",
                table: "TypologyChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Signatures",
                table: "Signatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servers",
                table: "Servers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rollbackpres",
                table: "Rollbackpres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollbackPlans",
                table: "RollbackPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponsibleAreas",
                table: "ResponsibleAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestTypes",
                table: "RequestTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileUsers",
                table: "ProfileUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prerrequisitos",
                table: "Prerrequisitos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postimplantacions",
                table: "Postimplantacions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FunctionalUsers",
                table: "FunctionalUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Environments",
                table: "Environments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Changes",
                table: "Changes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeAplicatives",
                table: "ChangeAplicatives");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAplicatives_EnvironmentID",
                table: "ChangeAplicatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blueprints",
                table: "Blueprints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicatives",
                table: "Applicatives");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "Prerrequisitos");

            migrationBuilder.DropColumn(
                name: "RollbackPreID",
                table: "Prerrequisitos");

            migrationBuilder.DropColumn(
                name: "SupplierArea",
                table: "Prerrequisitos");

            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "supplierArea",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "changeType",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "changerNumber",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "EnvironmentID",
                table: "ChangeAplicatives");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserChanges",
                newName: "UserChange");

            migrationBuilder.RenameTable(
                name: "TypologyChanges",
                newName: "TypologyChange");

            migrationBuilder.RenameTable(
                name: "Trainings",
                newName: "Training");

            migrationBuilder.RenameTable(
                name: "Signatures",
                newName: "Signature");

            migrationBuilder.RenameTable(
                name: "Servers",
                newName: "Server");

            migrationBuilder.RenameTable(
                name: "Rollbackpres",
                newName: "Rollbackpre");

            migrationBuilder.RenameTable(
                name: "RollbackPlans",
                newName: "RollbackPlan");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "Result");

            migrationBuilder.RenameTable(
                name: "ResponsibleAreas",
                newName: "ResponsibleArea");

            migrationBuilder.RenameTable(
                name: "RequestTypes",
                newName: "RequestType");

            migrationBuilder.RenameTable(
                name: "ProfileUsers",
                newName: "ProfileUser");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profile");

            migrationBuilder.RenameTable(
                name: "Prerrequisitos",
                newName: "Prerrequisito");

            migrationBuilder.RenameTable(
                name: "Postimplantacions",
                newName: "Postimplantacion");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "Plan");

            migrationBuilder.RenameTable(
                name: "FunctionalUsers",
                newName: "FunctionalUser");

            migrationBuilder.RenameTable(
                name: "Environments",
                newName: "Environment");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Contact");

            migrationBuilder.RenameTable(
                name: "Changes",
                newName: "Change");

            migrationBuilder.RenameTable(
                name: "ChangeAplicatives",
                newName: "ChangeAplicative");

            migrationBuilder.RenameTable(
                name: "Blueprints",
                newName: "Blueprint");

            migrationBuilder.RenameTable(
                name: "Applicatives",
                newName: "Applicative");

            migrationBuilder.RenameIndex(
                name: "IX_UserChanges_UserID",
                table: "UserChange",
                newName: "IX_UserChange_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserChanges_ChangeID",
                table: "UserChange",
                newName: "IX_UserChange_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_UserID",
                table: "Training",
                newName: "IX_Training_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_ChangesChangeID",
                table: "Training",
                newName: "IX_Training_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_ChangeID",
                table: "Training",
                newName: "IX_Training_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Signatures_UserID",
                table: "Signature",
                newName: "IX_Signature_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Signatures_ChangesChangeID",
                table: "Signature",
                newName: "IX_Signature_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Signatures_ChangeID",
                table: "Signature",
                newName: "IX_Signature_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Rollbackpres_PrerrequisitoID",
                table: "Rollbackpre",
                newName: "IX_Rollbackpre_PrerrequisitoID");

            migrationBuilder.RenameIndex(
                name: "IX_RollbackPlans_PlanID",
                table: "RollbackPlan",
                newName: "IX_RollbackPlan_PlanID");

            migrationBuilder.RenameIndex(
                name: "IX_Results_ChangesChangeID",
                table: "Result",
                newName: "IX_Result_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Results_ChangeID",
                table: "Result",
                newName: "IX_Result_ChangeID");

            migrationBuilder.RenameColumn(
                name: "ResponsibleAreaId",
                table: "ResponsibleArea",
                newName: "ResponsibleAreaID");

            migrationBuilder.RenameColumn(
                name: "Responsible",
                table: "ResponsibleArea",
                newName: "ResponsibleName");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "ResponsibleArea",
                newName: "AreaName");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileUsers_UserID",
                table: "ProfileUser",
                newName: "IX_ProfileUser_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileUsers_ProfileID",
                table: "ProfileUser",
                newName: "IX_ProfileUser_ProfileID");

            migrationBuilder.RenameColumn(
                name: "TypeofProfile",
                table: "Profile",
                newName: "ProfileName");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisitos_UserID",
                table: "Prerrequisito",
                newName: "IX_Prerrequisito_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisitos_ChangesChangeID",
                table: "Prerrequisito",
                newName: "IX_Prerrequisito_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisitos_ChangeID",
                table: "Prerrequisito",
                newName: "IX_Prerrequisito_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacions_UserID",
                table: "Postimplantacion",
                newName: "IX_Postimplantacion_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacions_ChangesChangeID",
                table: "Postimplantacion",
                newName: "IX_Postimplantacion_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacions_ChangeID",
                table: "Postimplantacion",
                newName: "IX_Postimplantacion_ChangeID");

            migrationBuilder.RenameColumn(
                name: "RollbackPlanID",
                table: "Plan",
                newName: "ResponsibleAreaID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ChangesChangeID",
                table: "Plan",
                newName: "IX_Plan_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ChangeID",
                table: "Plan",
                newName: "IX_Plan_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUsers_UserID",
                table: "FunctionalUser",
                newName: "IX_FunctionalUser_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUsers_ChangesChangeID",
                table: "FunctionalUser",
                newName: "IX_FunctionalUser_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUsers_ChangeID",
                table: "FunctionalUser",
                newName: "IX_FunctionalUser_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserID",
                table: "Contact",
                newName: "IX_Contact_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_ChangesChangeID",
                table: "Contact",
                newName: "IX_Contact_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_ChangeID",
                table: "Contact",
                newName: "IX_Contact_ChangeID");

            migrationBuilder.RenameColumn(
                name: "modificationDate",
                table: "Change",
                newName: "ModificationDate");

            migrationBuilder.RenameColumn(
                name: "deploymentDate",
                table: "Change",
                newName: "DeploymentDate");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Change",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "checkList",
                table: "Change",
                newName: "CheckList");

            migrationBuilder.RenameColumn(
                name: "changeDescription",
                table: "Change",
                newName: "ChangeDescription");

            migrationBuilder.RenameColumn(
                name: "applicationDate",
                table: "Change",
                newName: "ApplicationDate");

            migrationBuilder.RenameColumn(
                name: "requestType",
                table: "Change",
                newName: "ChangeNumber");

            migrationBuilder.RenameColumn(
                name: "ChangeApplicativeID",
                table: "Change",
                newName: "TypologyChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_UserID",
                table: "Change",
                newName: "IX_Change_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_ChangeApplicativeID",
                table: "Change",
                newName: "IX_Change_TypologyChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAplicatives_ApplicativeID",
                table: "ChangeAplicative",
                newName: "IX_ChangeAplicative_ApplicativeID");

            migrationBuilder.RenameIndex(
                name: "IX_Blueprints_ChangeID",
                table: "Blueprint",
                newName: "IX_Blueprint_ChangeID");

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Training",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "ProfileUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleAreaID",
                table: "Prerrequisito",
                type: "int",
                maxLength: 5000,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "Environment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentID",
                table: "Change",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestTypeID",
                table: "Change",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChange",
                table: "UserChange",
                column: "UserChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyChange",
                table: "TypologyChange",
                column: "TypologyChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Training",
                table: "Training",
                column: "TrainingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signature",
                table: "Signature",
                column: "SignatureID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Server",
                table: "Server",
                column: "ServerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rollbackpre",
                table: "Rollbackpre",
                column: "RollbackPreID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollbackPlan",
                table: "RollbackPlan",
                column: "RollbackPlanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Result",
                table: "Result",
                column: "ResultID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponsibleArea",
                table: "ResponsibleArea",
                column: "ResponsibleAreaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestType",
                table: "RequestType",
                column: "RequestTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileUser",
                table: "ProfileUser",
                column: "ProfileUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "ProfileID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prerrequisito",
                table: "Prerrequisito",
                column: "PrerrequisitoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postimplantacion",
                table: "Postimplantacion",
                column: "PostimplantacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan",
                table: "Plan",
                column: "PlanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FunctionalUser",
                table: "FunctionalUser",
                column: "FunctionalUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Environment",
                table: "Environment",
                column: "EnvironmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "ContactID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Change",
                table: "Change",
                column: "ChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeAplicative",
                table: "ChangeAplicative",
                column: "ChangeApplicativeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blueprint",
                table: "Blueprint",
                column: "BlueprintID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicative",
                table: "Applicative",
                column: "ApplicativeID");

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.TypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Training_TypeID",
                table: "Training",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisito_ResponsibleAreaID",
                table: "Prerrequisito",
                column: "ResponsibleAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ResponsibleAreaID",
                table: "Plan",
                column: "ResponsibleAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Environment_ServerID",
                table: "Environment",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_Change_EnvironmentID",
                table: "Change",
                column: "EnvironmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Change_RequestTypeID",
                table: "Change",
                column: "RequestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicative_ChangeID",
                table: "ChangeAplicative",
                column: "ChangeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blueprint_Change_ChangeID",
                table: "Blueprint",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Change_Environment_EnvironmentID",
                table: "Change",
                column: "EnvironmentID",
                principalTable: "Environment",
                principalColumn: "EnvironmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Change_RequestType_RequestTypeID",
                table: "Change",
                column: "RequestTypeID",
                principalTable: "RequestType",
                principalColumn: "RequestTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Change_TypologyChange_TypologyChangeID",
                table: "Change",
                column: "TypologyChangeID",
                principalTable: "TypologyChange",
                principalColumn: "TypologyChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Change_User_UserID",
                table: "Change",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Change_ChangeID",
                table: "Contact",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Change_ChangesChangeID",
                table: "Contact",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserID",
                table: "Contact",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Environment_Server_ServerID",
                table: "Environment",
                column: "ServerID",
                principalTable: "Server",
                principalColumn: "ServerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUser_Change_ChangeID",
                table: "FunctionalUser",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUser_Change_ChangesChangeID",
                table: "FunctionalUser",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUser_User_UserID",
                table: "FunctionalUser",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Change_ChangeID",
                table: "Plan",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Change_ChangesChangeID",
                table: "Plan",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_ResponsibleArea_ResponsibleAreaID",
                table: "Plan",
                column: "ResponsibleAreaID",
                principalTable: "ResponsibleArea",
                principalColumn: "ResponsibleAreaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacion_Change_ChangeID",
                table: "Postimplantacion",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacion_Change_ChangesChangeID",
                table: "Postimplantacion",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacion_User_UserID",
                table: "Postimplantacion",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisito_Change_ChangeID",
                table: "Prerrequisito",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisito_Change_ChangesChangeID",
                table: "Prerrequisito",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisito_ResponsibleArea_ResponsibleAreaID",
                table: "Prerrequisito",
                column: "ResponsibleAreaID",
                principalTable: "ResponsibleArea",
                principalColumn: "ResponsibleAreaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisito_User_UserID",
                table: "Prerrequisito",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUser_Profile_ProfileID",
                table: "ProfileUser",
                column: "ProfileID",
                principalTable: "Profile",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUser_User_UserID",
                table: "ProfileUser",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Change_ChangeID",
                table: "Result",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Change_ChangesChangeID",
                table: "Result",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RollbackPlan_Plan_PlanID",
                table: "RollbackPlan",
                column: "PlanID",
                principalTable: "Plan",
                principalColumn: "PlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rollbackpre_Prerrequisito_PrerrequisitoID",
                table: "Rollbackpre",
                column: "PrerrequisitoID",
                principalTable: "Prerrequisito",
                principalColumn: "PrerrequisitoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_Change_ChangeID",
                table: "Signature",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_Change_ChangesChangeID",
                table: "Signature",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_User_UserID",
                table: "Signature",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Change_ChangeID",
                table: "Training",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Change_ChangesChangeID",
                table: "Training",
                column: "ChangesChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Type_TypeID",
                table: "Training",
                column: "TypeID",
                principalTable: "Type",
                principalColumn: "TypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_User_UserID",
                table: "Training",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChange_Change_ChangeID",
                table: "UserChange",
                column: "ChangeID",
                principalTable: "Change",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChange_User_UserID",
                table: "UserChange",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blueprint_Change_ChangeID",
                table: "Blueprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Change_Environment_EnvironmentID",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Change_RequestType_RequestTypeID",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Change_TypologyChange_TypologyChangeID",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Change_User_UserID",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicative_Applicative_ApplicativeID",
                table: "ChangeAplicative");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAplicative_Change_ChangeID",
                table: "ChangeAplicative");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Change_ChangeID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Change_ChangesChangeID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UserID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Environment_Server_ServerID",
                table: "Environment");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUser_Change_ChangeID",
                table: "FunctionalUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUser_Change_ChangesChangeID",
                table: "FunctionalUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FunctionalUser_User_UserID",
                table: "FunctionalUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Change_ChangeID",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Change_ChangesChangeID",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_Plan_ResponsibleArea_ResponsibleAreaID",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacion_Change_ChangeID",
                table: "Postimplantacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacion_Change_ChangesChangeID",
                table: "Postimplantacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Postimplantacion_User_UserID",
                table: "Postimplantacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisito_Change_ChangeID",
                table: "Prerrequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisito_Change_ChangesChangeID",
                table: "Prerrequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisito_ResponsibleArea_ResponsibleAreaID",
                table: "Prerrequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerrequisito_User_UserID",
                table: "Prerrequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUser_Profile_ProfileID",
                table: "ProfileUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUser_User_UserID",
                table: "ProfileUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Result_Change_ChangeID",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_Result_Change_ChangesChangeID",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_RollbackPlan_Plan_PlanID",
                table: "RollbackPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Rollbackpre_Prerrequisito_PrerrequisitoID",
                table: "Rollbackpre");

            migrationBuilder.DropForeignKey(
                name: "FK_Signature_Change_ChangeID",
                table: "Signature");

            migrationBuilder.DropForeignKey(
                name: "FK_Signature_Change_ChangesChangeID",
                table: "Signature");

            migrationBuilder.DropForeignKey(
                name: "FK_Signature_User_UserID",
                table: "Signature");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Change_ChangeID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Change_ChangesChangeID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Type_TypeID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_User_UserID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChange_Change_ChangeID",
                table: "UserChange");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChange_User_UserID",
                table: "UserChange");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChange",
                table: "UserChange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyChange",
                table: "TypologyChange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Training",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_Training_TypeID",
                table: "Training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Signature",
                table: "Signature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Server",
                table: "Server");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rollbackpre",
                table: "Rollbackpre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RollbackPlan",
                table: "RollbackPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Result",
                table: "Result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponsibleArea",
                table: "ResponsibleArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestType",
                table: "RequestType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileUser",
                table: "ProfileUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prerrequisito",
                table: "Prerrequisito");

            migrationBuilder.DropIndex(
                name: "IX_Prerrequisito_ResponsibleAreaID",
                table: "Prerrequisito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postimplantacion",
                table: "Postimplantacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan",
                table: "Plan");

            migrationBuilder.DropIndex(
                name: "IX_Plan_ResponsibleAreaID",
                table: "Plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FunctionalUser",
                table: "FunctionalUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Environment",
                table: "Environment");

            migrationBuilder.DropIndex(
                name: "IX_Environment_ServerID",
                table: "Environment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeAplicative",
                table: "ChangeAplicative");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAplicative_ChangeID",
                table: "ChangeAplicative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Change",
                table: "Change");

            migrationBuilder.DropIndex(
                name: "IX_Change_EnvironmentID",
                table: "Change");

            migrationBuilder.DropIndex(
                name: "IX_Change_RequestTypeID",
                table: "Change");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blueprint",
                table: "Blueprint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicative",
                table: "Applicative");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ProfileUser");

            migrationBuilder.DropColumn(
                name: "ResponsibleAreaID",
                table: "Prerrequisito");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "Environment");

            migrationBuilder.DropColumn(
                name: "EnvironmentID",
                table: "Change");

            migrationBuilder.DropColumn(
                name: "RequestTypeID",
                table: "Change");

            migrationBuilder.RenameTable(
                name: "UserChange",
                newName: "UserChanges");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TypologyChange",
                newName: "TypologyChanges");

            migrationBuilder.RenameTable(
                name: "Training",
                newName: "Trainings");

            migrationBuilder.RenameTable(
                name: "Signature",
                newName: "Signatures");

            migrationBuilder.RenameTable(
                name: "Server",
                newName: "Servers");

            migrationBuilder.RenameTable(
                name: "Rollbackpre",
                newName: "Rollbackpres");

            migrationBuilder.RenameTable(
                name: "RollbackPlan",
                newName: "RollbackPlans");

            migrationBuilder.RenameTable(
                name: "Result",
                newName: "Results");

            migrationBuilder.RenameTable(
                name: "ResponsibleArea",
                newName: "ResponsibleAreas");

            migrationBuilder.RenameTable(
                name: "RequestType",
                newName: "RequestTypes");

            migrationBuilder.RenameTable(
                name: "ProfileUser",
                newName: "ProfileUsers");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "Prerrequisito",
                newName: "Prerrequisitos");

            migrationBuilder.RenameTable(
                name: "Postimplantacion",
                newName: "Postimplantacions");

            migrationBuilder.RenameTable(
                name: "Plan",
                newName: "Plans");

            migrationBuilder.RenameTable(
                name: "FunctionalUser",
                newName: "FunctionalUsers");

            migrationBuilder.RenameTable(
                name: "Environment",
                newName: "Environments");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contacts");

            migrationBuilder.RenameTable(
                name: "ChangeAplicative",
                newName: "ChangeAplicatives");

            migrationBuilder.RenameTable(
                name: "Change",
                newName: "Changes");

            migrationBuilder.RenameTable(
                name: "Blueprint",
                newName: "Blueprints");

            migrationBuilder.RenameTable(
                name: "Applicative",
                newName: "Applicatives");

            migrationBuilder.RenameIndex(
                name: "IX_UserChange_UserID",
                table: "UserChanges",
                newName: "IX_UserChanges_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserChange_ChangeID",
                table: "UserChanges",
                newName: "IX_UserChanges_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Training_UserID",
                table: "Trainings",
                newName: "IX_Trainings_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Training_ChangesChangeID",
                table: "Trainings",
                newName: "IX_Trainings_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Training_ChangeID",
                table: "Trainings",
                newName: "IX_Trainings_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Signature_UserID",
                table: "Signatures",
                newName: "IX_Signatures_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Signature_ChangesChangeID",
                table: "Signatures",
                newName: "IX_Signatures_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Signature_ChangeID",
                table: "Signatures",
                newName: "IX_Signatures_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Rollbackpre_PrerrequisitoID",
                table: "Rollbackpres",
                newName: "IX_Rollbackpres_PrerrequisitoID");

            migrationBuilder.RenameIndex(
                name: "IX_RollbackPlan_PlanID",
                table: "RollbackPlans",
                newName: "IX_RollbackPlans_PlanID");

            migrationBuilder.RenameIndex(
                name: "IX_Result_ChangesChangeID",
                table: "Results",
                newName: "IX_Results_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Result_ChangeID",
                table: "Results",
                newName: "IX_Results_ChangeID");

            migrationBuilder.RenameColumn(
                name: "ResponsibleAreaID",
                table: "ResponsibleAreas",
                newName: "ResponsibleAreaId");

            migrationBuilder.RenameColumn(
                name: "ResponsibleName",
                table: "ResponsibleAreas",
                newName: "Responsible");

            migrationBuilder.RenameColumn(
                name: "AreaName",
                table: "ResponsibleAreas",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileUser_UserID",
                table: "ProfileUsers",
                newName: "IX_ProfileUsers_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileUser_ProfileID",
                table: "ProfileUsers",
                newName: "IX_ProfileUsers_ProfileID");

            migrationBuilder.RenameColumn(
                name: "ProfileName",
                table: "Profiles",
                newName: "TypeofProfile");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisito_UserID",
                table: "Prerrequisitos",
                newName: "IX_Prerrequisitos_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisito_ChangesChangeID",
                table: "Prerrequisitos",
                newName: "IX_Prerrequisitos_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Prerrequisito_ChangeID",
                table: "Prerrequisitos",
                newName: "IX_Prerrequisitos_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacion_UserID",
                table: "Postimplantacions",
                newName: "IX_Postimplantacions_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacion_ChangesChangeID",
                table: "Postimplantacions",
                newName: "IX_Postimplantacions_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Postimplantacion_ChangeID",
                table: "Postimplantacions",
                newName: "IX_Postimplantacions_ChangeID");

            migrationBuilder.RenameColumn(
                name: "ResponsibleAreaID",
                table: "Plans",
                newName: "RollbackPlanID");

            migrationBuilder.RenameIndex(
                name: "IX_Plan_ChangesChangeID",
                table: "Plans",
                newName: "IX_Plans_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Plan_ChangeID",
                table: "Plans",
                newName: "IX_Plans_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUser_UserID",
                table: "FunctionalUsers",
                newName: "IX_FunctionalUsers_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUser_ChangesChangeID",
                table: "FunctionalUsers",
                newName: "IX_FunctionalUsers_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_FunctionalUser_ChangeID",
                table: "FunctionalUsers",
                newName: "IX_FunctionalUsers_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_UserID",
                table: "Contacts",
                newName: "IX_Contacts_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ChangesChangeID",
                table: "Contacts",
                newName: "IX_Contacts_ChangesChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ChangeID",
                table: "Contacts",
                newName: "IX_Contacts_ChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAplicative_ApplicativeID",
                table: "ChangeAplicatives",
                newName: "IX_ChangeAplicatives_ApplicativeID");

            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "Changes",
                newName: "modificationDate");

            migrationBuilder.RenameColumn(
                name: "DeploymentDate",
                table: "Changes",
                newName: "deploymentDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Changes",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CheckList",
                table: "Changes",
                newName: "checkList");

            migrationBuilder.RenameColumn(
                name: "ChangeDescription",
                table: "Changes",
                newName: "changeDescription");

            migrationBuilder.RenameColumn(
                name: "ApplicationDate",
                table: "Changes",
                newName: "applicationDate");

            migrationBuilder.RenameColumn(
                name: "TypologyChangeID",
                table: "Changes",
                newName: "ChangeApplicativeID");

            migrationBuilder.RenameColumn(
                name: "ChangeNumber",
                table: "Changes",
                newName: "requestType");

            migrationBuilder.RenameIndex(
                name: "IX_Change_UserID",
                table: "Changes",
                newName: "IX_Changes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Change_TypologyChangeID",
                table: "Changes",
                newName: "IX_Changes_ChangeApplicativeID");

            migrationBuilder.RenameIndex(
                name: "IX_Blueprint_ChangeID",
                table: "Blueprints",
                newName: "IX_Blueprints_ChangeID");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Trainings",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Profiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "Prerrequisitos",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RollbackPreID",
                table: "Prerrequisitos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SupplierArea",
                table: "Prerrequisitos",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "Plans",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "supplierArea",
                table: "Plans",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentID",
                table: "ChangeAplicatives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "changeType",
                table: "Changes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "changerNumber",
                table: "Changes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChanges",
                table: "UserChanges",
                column: "UserChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyChanges",
                table: "TypologyChanges",
                column: "TypologyChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings",
                column: "TrainingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signatures",
                table: "Signatures",
                column: "SignatureID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servers",
                table: "Servers",
                column: "ServerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rollbackpres",
                table: "Rollbackpres",
                column: "RollbackPreID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RollbackPlans",
                table: "RollbackPlans",
                column: "RollbackPlanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "ResultID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponsibleAreas",
                table: "ResponsibleAreas",
                column: "ResponsibleAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestTypes",
                table: "RequestTypes",
                column: "RequestTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileUsers",
                table: "ProfileUsers",
                column: "ProfileUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "ProfileID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prerrequisitos",
                table: "Prerrequisitos",
                column: "PrerrequisitoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postimplantacions",
                table: "Postimplantacions",
                column: "PostimplantacionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "PlanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FunctionalUsers",
                table: "FunctionalUsers",
                column: "FunctionalUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Environments",
                table: "Environments",
                column: "EnvironmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "ContactID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeAplicatives",
                table: "ChangeAplicatives",
                column: "ChangeApplicativeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Changes",
                table: "Changes",
                column: "ChangeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blueprints",
                table: "Blueprints",
                column: "BlueprintID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicatives",
                table: "Applicatives",
                column: "ApplicativeID");

            migrationBuilder.CreateTable(
                name: "EnvironmentApplicatives",
                columns: table => new
                {
                    EnvironmentApplicativeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicativeID = table.Column<int>(type: "int", nullable: false),
                    EnvironmentID = table.Column<int>(type: "int", nullable: false),
                    ServerID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAplicatives_EnvironmentID",
                table: "ChangeAplicatives",
                column: "EnvironmentID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Blueprints_Changes_ChangeID",
                table: "Blueprints",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAplicatives_Applicatives_ApplicativeID",
                table: "ChangeAplicatives",
                column: "ApplicativeID",
                principalTable: "Applicatives",
                principalColumn: "ApplicativeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAplicatives_Environments_EnvironmentID",
                table: "ChangeAplicatives",
                column: "EnvironmentID",
                principalTable: "Environments",
                principalColumn: "EnvironmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ChangeAplicatives_ChangeApplicativeID",
                table: "Changes",
                column: "ChangeApplicativeID",
                principalTable: "ChangeAplicatives",
                principalColumn: "ChangeApplicativeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Users_UserID",
                table: "Changes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Changes_ChangeID",
                table: "Contacts",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Changes_ChangesChangeID",
                table: "Contacts",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserID",
                table: "Contacts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUsers_Changes_ChangeID",
                table: "FunctionalUsers",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUsers_Changes_ChangesChangeID",
                table: "FunctionalUsers",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FunctionalUsers_Users_UserID",
                table: "FunctionalUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Changes_ChangeID",
                table: "Plans",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Changes_ChangesChangeID",
                table: "Plans",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacions_Changes_ChangeID",
                table: "Postimplantacions",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacions_Changes_ChangesChangeID",
                table: "Postimplantacions",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postimplantacions_Users_UserID",
                table: "Postimplantacions",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisitos_Changes_ChangeID",
                table: "Prerrequisitos",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisitos_Changes_ChangesChangeID",
                table: "Prerrequisitos",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerrequisitos_Users_UserID",
                table: "Prerrequisitos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUsers_Profiles_ProfileID",
                table: "ProfileUsers",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUsers_Users_UserID",
                table: "ProfileUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Changes_ChangeID",
                table: "Results",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Changes_ChangesChangeID",
                table: "Results",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RollbackPlans_Plans_PlanID",
                table: "RollbackPlans",
                column: "PlanID",
                principalTable: "Plans",
                principalColumn: "PlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rollbackpres_Prerrequisitos_PrerrequisitoID",
                table: "Rollbackpres",
                column: "PrerrequisitoID",
                principalTable: "Prerrequisitos",
                principalColumn: "PrerrequisitoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_Changes_ChangeID",
                table: "Signatures",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_Changes_ChangesChangeID",
                table: "Signatures",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_Users_UserID",
                table: "Signatures",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Changes_ChangeID",
                table: "Trainings",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Changes_ChangesChangeID",
                table: "Trainings",
                column: "ChangesChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Users_UserID",
                table: "Trainings",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanges_Changes_ChangeID",
                table: "UserChanges",
                column: "ChangeID",
                principalTable: "Changes",
                principalColumn: "ChangeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanges_Users_UserID",
                table: "UserChanges",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
