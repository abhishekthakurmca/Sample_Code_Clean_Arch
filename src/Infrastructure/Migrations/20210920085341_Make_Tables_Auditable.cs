using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Make_Tables_Auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "WGSAccesrails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WGSAccesrails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "WGSAccesrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "WGSAccesrails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TeamMemberShipments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TeamMemberShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "TeamMemberShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "TeamMemberShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ScheduleDates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ScheduleDates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ScheduleDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ScheduleDates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductCategoryWeights",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductCategoryWeights",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductCategoryWeights",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductCategoryWeights",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ManageClients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ManageClients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ManageClients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ManageClients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LU_Truck",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LU_Truck",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "LU_Truck",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "LU_Truck",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LU_TeamMember",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LU_TeamMember",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "LU_TeamMember",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "LU_TeamMember",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LU_State",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LU_State",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "LU_State",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "LU_State",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LU_Permissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LU_Permissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "LU_Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "LU_Permissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LU_Country",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LU_Country",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "LU_Country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "LU_Country",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "FreightPermissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FreightPermissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "FreightPermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FreightPermissions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "FreightCompanyCodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FreightCompanyCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "FreightCompanyCodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FreightCompanyCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "FreightCategory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FreightCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "FreightCategory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FreightCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DockScheduled",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DockScheduled",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "DockScheduled",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DockScheduled",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CompanyDocuments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CompanyDocuments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CompanyDocuments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CompanyDocuments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CompanyContact",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CompanyContact",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CompanyContact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CompanyContact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "WGSAccesrails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WGSAccesrails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "WGSAccesrails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "WGSAccesrails");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TeamMemberShipments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TeamMemberShipments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "TeamMemberShipments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "TeamMemberShipments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ScheduleDates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ScheduleDates");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ScheduleDates");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ScheduleDates");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductCategoryWeights");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductCategoryWeights");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductCategoryWeights");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductCategoryWeights");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LU_Truck");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LU_Truck");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "LU_Truck");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "LU_Truck");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LU_TeamMember");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LU_TeamMember");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "LU_TeamMember");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "LU_TeamMember");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LU_State");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LU_State");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "LU_State");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "LU_State");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LU_Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LU_Permissions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "LU_Permissions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "LU_Permissions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LU_Country");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LU_Country");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "LU_Country");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "LU_Country");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "FreightPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FreightPermissions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "FreightPermissions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FreightPermissions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "FreightCompanyCodes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FreightCompanyCodes");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "FreightCompanyCodes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FreightCompanyCodes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "FreightCategory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FreightCategory");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "FreightCategory");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FreightCategory");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CompanyContact");
        }
    }
}
