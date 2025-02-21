using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceBookingSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModelRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ResourceId",
                table: "Bookings",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ResourceId",
                table: "Bookings");
        }
    }
}
