using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorStudio_api.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Reservation_AspNetUsers_UserId1",
               table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_UserId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
