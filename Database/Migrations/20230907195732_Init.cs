using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserKeyMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Salt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    IV = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKeyMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Firstname = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Surname = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaultLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VaultId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Description = table.Column<byte[]>(type: "BLOB", nullable: true),
                    URL = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Email = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Username = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Password = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Notes = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Category = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaultLogins_Vaults_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaultNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VaultId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Note = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Description = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UpdatedDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaultNotes_Vaults_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserKeyMetadata_Email",
                table: "UserKeyMetadata",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaultLogins_VaultId",
                table: "VaultLogins",
                column: "VaultId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultNotes_VaultId",
                table: "VaultNotes",
                column: "VaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaults_UserId",
                table: "Vaults",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKeyMetadata");

            migrationBuilder.DropTable(
                name: "VaultLogins");

            migrationBuilder.DropTable(
                name: "VaultNotes");

            migrationBuilder.DropTable(
                name: "Vaults");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
