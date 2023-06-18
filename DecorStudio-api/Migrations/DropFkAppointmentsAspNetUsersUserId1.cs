using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorStudio_api.Migrations
{
    /// <inheritdoc />
    public partial class DropFkAppointmentsAspNetUsersUserId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_UserId1",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Vratite spoljni ključ i indeks ako je potrebno
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId1",
                table: "Appointments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId1",
                table: "Appointments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
