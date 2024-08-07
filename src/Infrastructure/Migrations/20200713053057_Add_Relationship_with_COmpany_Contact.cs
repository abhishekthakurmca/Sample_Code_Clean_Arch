using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Relationship_with_COmpany_Contact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "ContactTypeID",
                table: "CompanyContact");

            migrationBuilder.AlterColumn<int>(
                name: "FreightCompanyId",
                table: "CompanyContact",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FreightCompanyId1",
                table: "CompanyContact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LU_ClientContactTypesId",
                table: "CompanyContact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_FreightCompanyId1",
                table: "CompanyContact",
                column: "FreightCompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_LU_ClientContactTypesId",
                table: "CompanyContact",
                column: "LU_ClientContactTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId1",
                table: "CompanyContact",
                column: "FreightCompanyId1",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_LU_ClientContactTypes_LU_ClientContactTypesId",
                table: "CompanyContact",
                column: "LU_ClientContactTypesId",
                principalTable: "LU_ClientContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId1",
                table: "CompanyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_LU_ClientContactTypes_LU_ClientContactTypesId",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_FreightCompanyId1",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_LU_ClientContactTypesId",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "FreightCompanyId1",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "LU_ClientContactTypesId",
                table: "CompanyContact");

            migrationBuilder.AlterColumn<long>(
                name: "FreightCompanyId",
                table: "CompanyContact",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "CompanyContact",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ContactTypeID",
                table: "CompanyContact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
