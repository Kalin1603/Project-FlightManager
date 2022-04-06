using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class ModifyModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passanger_Reservations_ReservationId",
                table: "Passanger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passanger",
                table: "Passanger");

            migrationBuilder.RenameTable(
                name: "Passanger",
                newName: "Passangers");

            migrationBuilder.RenameIndex(
                name: "IX_Passanger_ReservationId",
                table: "Passangers",
                newName: "IX_Passangers_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passangers",
                table: "Passangers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passangers_Reservations_ReservationId",
                table: "Passangers",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passangers_Reservations_ReservationId",
                table: "Passangers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passangers",
                table: "Passangers");

            migrationBuilder.RenameTable(
                name: "Passangers",
                newName: "Passanger");

            migrationBuilder.RenameIndex(
                name: "IX_Passangers_ReservationId",
                table: "Passanger",
                newName: "IX_Passanger_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passanger",
                table: "Passanger",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passanger_Reservations_ReservationId",
                table: "Passanger",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
