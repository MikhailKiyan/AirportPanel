using Microsoft.EntityFrameworkCore.Migrations;

namespace AirportPanel.DAL.Migrations
{
    public partial class iii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "FlightType",
                table: "Flights",
                nullable: true,
                oldClrType: typeof(byte));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "FlightType",
                table: "Flights",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);
        }
    }
}
