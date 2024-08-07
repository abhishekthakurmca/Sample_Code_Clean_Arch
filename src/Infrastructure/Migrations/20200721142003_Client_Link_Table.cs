using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Client_Link_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultPrice",
                table: "WGSAccesrails",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "CompanyLinkedClients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Company_Id = table.Column<long>(nullable: false),
                    ClientId = table.Column<long>(nullable: false),
                    ClientName = table.Column<string>(nullable: false),
                    LocationId = table.Column<long>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    PackageId = table.Column<long>(nullable: false),
                    PackageName = table.Column<string>(nullable: true),
                    FreightType = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsSecondary = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLinkedClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLinkedClients_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLinkedClients_Company_Id",
                table: "CompanyLinkedClients",
                column: "Company_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLinkedClients");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultPrice",
                table: "WGSAccesrails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");
        }
    }
}
