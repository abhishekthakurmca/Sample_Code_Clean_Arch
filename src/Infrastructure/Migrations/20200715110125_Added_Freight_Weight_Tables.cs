using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Freight_Weight_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "IsChannelSalesRep",
                table: "CompanyContact");

            migrationBuilder.AddColumn<bool>(
                name: "FTL",
                table: "FreightCompany",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LTL",
                table: "FreightCompany",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WGS",
                table: "FreightCompany",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FTL_Company",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Company_Id = table.Column<long>(nullable: false),
                    OriginCity = table.Column<string>(maxLength: 100, nullable: false),
                    DestinationCity = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTL_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTL_Company_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LTL_Company",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Company_Id = table.Column<long>(nullable: false),
                    OriginCity = table.Column<string>(maxLength: 100, nullable: true),
                    DestinationCity = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PPrice1 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice2 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice3 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice4 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice5 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice6 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice7 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice8 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice9 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PPrice10 = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTL_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LTL_Company_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FTL_Company_Company_Id",
                table: "FTL_Company",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LTL_Company_Company_Id",
                table: "LTL_Company",
                column: "Company_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FTL_Company");

            migrationBuilder.DropTable(
                name: "LTL_Company");

            migrationBuilder.DropColumn(
                name: "FTL",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "LTL",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "WGS",
                table: "FreightCompany");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByID",
                table: "CompanyContact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChannelSalesRep",
                table: "CompanyContact",
                type: "bit",
                nullable: true);
        }
    }
}
