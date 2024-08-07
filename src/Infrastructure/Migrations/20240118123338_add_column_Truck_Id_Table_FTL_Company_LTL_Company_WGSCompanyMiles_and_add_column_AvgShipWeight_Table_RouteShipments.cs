using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class add_column_Truck_Id_Table_FTL_Company_LTL_Company_WGSCompanyMiles_and_add_column_AvgShipWeight_Table_RouteShipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoldOverNotes",
                table: "RouteShipments");

            migrationBuilder.AddColumn<int>(
                name: "Truck_Id",
                table: "WGSCompanyMiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvgShipWeight",
                table: "RouteShipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Truck_Id",
                table: "LTL_Company",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Truck_Id",
                table: "FTL_Company",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WGSCompanyMiles_Truck_Id",
                table: "WGSCompanyMiles",
                column: "Truck_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LTL_Company_Truck_Id",
                table: "LTL_Company",
                column: "Truck_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FTL_Company_Truck_Id",
                table: "FTL_Company",
                column: "Truck_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTL_Company_LU_Truck_Truck_Id",
                table: "FTL_Company",
                column: "Truck_Id",
                principalTable: "LU_Truck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LTL_Company_LU_Truck_Truck_Id",
                table: "LTL_Company",
                column: "Truck_Id",
                principalTable: "LU_Truck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WGSCompanyMiles_LU_Truck_Truck_Id",
                table: "WGSCompanyMiles",
                column: "Truck_Id",
                principalTable: "LU_Truck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FTL_Company_LU_Truck_Truck_Id",
                table: "FTL_Company");

            migrationBuilder.DropForeignKey(
                name: "FK_LTL_Company_LU_Truck_Truck_Id",
                table: "LTL_Company");

            migrationBuilder.DropForeignKey(
                name: "FK_WGSCompanyMiles_LU_Truck_Truck_Id",
                table: "WGSCompanyMiles");

            migrationBuilder.DropIndex(
                name: "IX_WGSCompanyMiles_Truck_Id",
                table: "WGSCompanyMiles");

            migrationBuilder.DropIndex(
                name: "IX_LTL_Company_Truck_Id",
                table: "LTL_Company");

            migrationBuilder.DropIndex(
                name: "IX_FTL_Company_Truck_Id",
                table: "FTL_Company");

            migrationBuilder.DropColumn(
                name: "Truck_Id",
                table: "WGSCompanyMiles");

            migrationBuilder.DropColumn(
                name: "AvgShipWeight",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Truck_Id",
                table: "LTL_Company");

            migrationBuilder.DropColumn(
                name: "Truck_Id",
                table: "FTL_Company");

            migrationBuilder.AddColumn<string>(
                name: "HoldOverNotes",
                table: "RouteShipments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
