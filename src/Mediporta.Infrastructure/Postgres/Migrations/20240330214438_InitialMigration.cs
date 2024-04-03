using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediporta.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Populations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Populations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimplifiedTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Share = table.Column<double>(type: "double precision", nullable: false),
                    PopulationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimplifiedTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimplifiedTags_Populations_PopulationId",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimplifiedTags_PopulationId",
                table: "SimplifiedTags",
                column: "PopulationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimplifiedTags");

            migrationBuilder.DropTable(
                name: "Populations");
        }
    }
}
