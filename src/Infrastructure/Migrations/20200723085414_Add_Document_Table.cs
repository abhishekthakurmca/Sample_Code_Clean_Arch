using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Document_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightType",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "CompanyLinkedClients");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovalRequired",
                table: "CompanyLinkedClients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CollectionId",
                table: "CompanyLinkedClients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CollectionName",
                table: "CompanyLinkedClients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScheduleFrequency",
                table: "CompanyLinkedClients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(nullable: false),
                    DocumentType = table.Column<string>(nullable: true),
                    DocumentPath = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_Company_Id",
                table: "CompanyDocuments",
                column: "Company_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "ApprovalRequired",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "CollectionName",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "ScheduleFrequency",
                table: "CompanyLinkedClients");

            migrationBuilder.AddColumn<string>(
                name: "FreightType",
                table: "CompanyLinkedClients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "CompanyLinkedClients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "CompanyLinkedClients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PackageId",
                table: "CompanyLinkedClients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "CompanyLinkedClients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
