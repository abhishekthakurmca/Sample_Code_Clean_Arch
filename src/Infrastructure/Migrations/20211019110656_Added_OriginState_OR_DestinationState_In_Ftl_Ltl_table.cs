using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_OriginState_OR_DestinationState_In_Ftl_Ltl_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationState",
                table: "LTL_Company",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginState",
                table: "LTL_Company",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationState",
                table: "FTL_Company",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginState",
                table: "FTL_Company",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationState",
                table: "LTL_Company");

            migrationBuilder.DropColumn(
                name: "OriginState",
                table: "LTL_Company");

            migrationBuilder.DropColumn(
                name: "DestinationState",
                table: "FTL_Company");

            migrationBuilder.DropColumn(
                name: "OriginState",
                table: "FTL_Company");
        }
    }
}
