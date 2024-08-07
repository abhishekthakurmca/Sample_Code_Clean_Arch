using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Tables_WGS_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WGSCompanyMiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Company_Id = table.Column<long>(nullable: false),
                    From = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    To = table.Column<long>(nullable: false),
                    LabelValue = table.Column<string>(unicode: false, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WGSCompanyMiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WGSCompanyMiles_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WGSCompanyWeights",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Company_Id = table.Column<long>(nullable: false),
                    From = table.Column<long>(nullable: false),
                    To = table.Column<long>(nullable: false),
                    LabelValue = table.Column<string>(unicode: false, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WGSCompanyWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WGSCompanyWeights_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WGSCompanyPrice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    CompanyWGSWeight_Id = table.Column<long>(nullable: false),
                    CompanyWGSMiles_Id = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WGSCompanyPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WGSCompanyPrice_WGSCompanyWeights_CompanyWGSWeight_Id",
                        column: x => x.CompanyWGSWeight_Id,
                        principalTable: "WGSCompanyWeights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WGSCompanyMiles_Company_Id",
                table: "WGSCompanyMiles",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WGSCompanyPrice_CompanyWGSWeight_Id",
                table: "WGSCompanyPrice",
                column: "CompanyWGSWeight_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WGSCompanyWeights_Company_Id",
                table: "WGSCompanyWeights",
                column: "Company_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WGSCompanyMiles");

            migrationBuilder.DropTable(
                name: "WGSCompanyPrice");

            migrationBuilder.DropTable(
                name: "WGSCompanyWeights");
        }
    }
}
