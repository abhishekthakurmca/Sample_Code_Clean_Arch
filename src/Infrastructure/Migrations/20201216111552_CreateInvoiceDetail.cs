using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class CreateInvoiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "100001, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FreightCompanyID = table.Column<int>(nullable: false),
                    SenderEmail = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    OrginalFileURL = table.Column<string>(nullable: true),
                    TotalLines = table.Column<int>(nullable: true),
                    FailedLines = table.Column<int>(nullable: true),
                    ImporAssettPrice = table.Column<float>(nullable: true),
                    FailedAssetPrice = table.Column<float>(nullable: true),
                    FailedFileURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetail");
        }
    }
}
