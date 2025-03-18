using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auhnuh_server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailForEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Episode",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Episode");
        }
    }
}
