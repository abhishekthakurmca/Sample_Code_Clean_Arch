using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Tables_WGS_Freight_Codess_PKkk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WGSCompanyMiles");

            migrationBuilder.DropTable(
                name: "WGSCompanyPrice");

            migrationBuilder.DropTable(
                name: "WGSCompanyWeights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WGSCompanyMiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Company_Id = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LabelValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    To = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Company_Id = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LabelValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWGSMiles_Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyWGSWeight_Id = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
    }
}
