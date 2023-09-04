using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class UserKeyMetadataEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserKeyMetadata_Email",
                table: "UserKeyMetadata");

            migrationBuilder.CreateIndex(
                name: "IX_UserKeyMetadata_Email",
                table: "UserKeyMetadata",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserKeyMetadata_Email",
                table: "UserKeyMetadata");

            migrationBuilder.CreateIndex(
                name: "IX_UserKeyMetadata_Email",
                table: "UserKeyMetadata",
                column: "Email");
        }
    }
}
