using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Accesrails_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WGSAccesrails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(nullable: false),
                    FreightCode_Id = table.Column<long>(nullable: false),
                    IsFixedPrice = table.Column<bool>(nullable: false),
                    DefaultPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WGSAccesrails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WGSAccesrails_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WGSAccesrails_FreightCompanyCodes_FreightCode_Id",
                        column: x => x.FreightCode_Id,
                        principalTable: "FreightCompanyCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WGSAccesrails_Company_Id",
                table: "WGSAccesrails",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WGSAccesrails_FreightCode_Id",
                table: "WGSAccesrails",
                column: "FreightCode_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WGSAccesrails");
        }
    }
}
