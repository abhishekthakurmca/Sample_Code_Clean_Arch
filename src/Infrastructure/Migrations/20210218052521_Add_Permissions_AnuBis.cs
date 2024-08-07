using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Permissions_AnuBis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail");

            migrationBuilder.AlterColumn<long>(
                name: "FreightCompanyId",
                table: "InvoiceDetail",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "LU_Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FreightPermision = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreightPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(nullable: false),
                    Permission_Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreightPermissions_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreightPermissions_LU_Permissions_Permission_Id",
                        column: x => x.Permission_Id,
                        principalTable: "LU_Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreightPermissions_Company_Id",
                table: "FreightPermissions",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FreightPermissions_Permission_Id",
                table: "FreightPermissions",
                column: "Permission_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "FreightPermissions");

            migrationBuilder.DropTable(
                name: "LU_Permissions");

            migrationBuilder.AlterColumn<long>(
                name: "FreightCompanyId",
                table: "InvoiceDetail",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
