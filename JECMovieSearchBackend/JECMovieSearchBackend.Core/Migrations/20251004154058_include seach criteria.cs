using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JECMovieSearchBackend.Core.Migrations
{
    /// <inheritdoc />
    public partial class includeseachcriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLastSearched",
                schema: "OMDB",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SearchQuery",
                schema: "OMDB",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "SearchQueries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastSearched = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchQueries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastSearched",
                schema: "OMDB",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SearchQuery",
                schema: "OMDB",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
