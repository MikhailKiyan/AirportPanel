using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirportPanel.DAL.Migrations
{
    public partial class migr0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    MofidiedOn = table.Column<DateTime>(nullable: true),
                    MofidiedBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Discription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    MofidiedOn = table.Column<DateTime>(nullable: true),
                    MofidiedBy = table.Column<Guid>(nullable: false),
                    FlightType = table.Column<byte>(nullable: true),
                    ArrivalOn = table.Column<DateTime>(nullable: true),
                    DepartureOn = table.Column<DateTime>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    StatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_FlightStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "FlightStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_StatusId",
                table: "Flights",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightStatuses");
        }
    }
}
