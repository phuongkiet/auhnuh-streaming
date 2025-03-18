using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auhnuh_server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCastAndDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Casts",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Directors",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Casts",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Directors",
                table: "Movie");
        }
    }
}
