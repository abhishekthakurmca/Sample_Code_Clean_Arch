using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class added_table_TeamMemberShipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMemberShipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<long>(nullable: false),
                    TeamMember_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMemberShipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMemberShipments_LU_TeamMember_TeamMember_Id",
                        column: x => x.TeamMember_Id,
                        principalTable: "LU_TeamMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberShipments_TeamMember_Id",
                table: "TeamMemberShipments",
                column: "TeamMember_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMemberShipments");
        }
    }
}
