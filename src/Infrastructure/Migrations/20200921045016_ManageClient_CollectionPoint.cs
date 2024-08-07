using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class ManageClient_CollectionPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectionPoint",
                table: "ManageClients",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CollectionPointID",
                table: "ManageClients",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionPoint",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "CollectionPointID",
                table: "ManageClients");
        }
    }
}
