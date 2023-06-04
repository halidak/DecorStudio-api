using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorStudio_api.Migrations
{
    /// <inheritdoc />
    public partial class reservationdecor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decor_Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    DecorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decor_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decor_Reservations_Decors_DecorId",
                        column: x => x.DecorId,
                        principalTable: "Decors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decor_Reservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Decor_Reservations_DecorId",
                table: "Decor_Reservations",
                column: "DecorId");

            migrationBuilder.CreateIndex(
                name: "IX_Decor_Reservations_ReservationId",
                table: "Decor_Reservations",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decor_Reservations");
        }
    }
}
