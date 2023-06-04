using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorStudio_api.Migrations
{
    /// <inheritdoc />
    public partial class catalogdecor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Decor_Decors_DecorId",
                table: "Warehouse_Decor");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Decor_Warehouses_WarehouseId",
                table: "Warehouse_Decor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse_Decor",
                table: "Warehouse_Decor");

            migrationBuilder.RenameTable(
                name: "Warehouse_Decor",
                newName: "Warehouse_Decors");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_Decor_WarehouseId",
                table: "Warehouse_Decors",
                newName: "IX_Warehouse_Decors_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_Decor_DecorId",
                table: "Warehouse_Decors",
                newName: "IX_Warehouse_Decors_DecorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse_Decors",
                table: "Warehouse_Decors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Catalog_Decors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(type: "int", nullable: false),
                    DecorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog_Decors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_Decors_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_Decors_Decors_DecorId",
                        column: x => x.DecorId,
                        principalTable: "Decors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_Decors_CatalogId",
                table: "Catalog_Decors",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_Decors_DecorId",
                table: "Catalog_Decors",
                column: "DecorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Decors_Decors_DecorId",
                table: "Warehouse_Decors",
                column: "DecorId",
                principalTable: "Decors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Decors_Warehouses_WarehouseId",
                table: "Warehouse_Decors",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Decors_Decors_DecorId",
                table: "Warehouse_Decors");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Decors_Warehouses_WarehouseId",
                table: "Warehouse_Decors");

            migrationBuilder.DropTable(
                name: "Catalog_Decors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse_Decors",
                table: "Warehouse_Decors");

            migrationBuilder.RenameTable(
                name: "Warehouse_Decors",
                newName: "Warehouse_Decor");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_Decors_WarehouseId",
                table: "Warehouse_Decor",
                newName: "IX_Warehouse_Decor_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_Decors_DecorId",
                table: "Warehouse_Decor",
                newName: "IX_Warehouse_Decor_DecorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse_Decor",
                table: "Warehouse_Decor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Decor_Decors_DecorId",
                table: "Warehouse_Decor",
                column: "DecorId",
                principalTable: "Decors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Decor_Warehouses_WarehouseId",
                table: "Warehouse_Decor",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
