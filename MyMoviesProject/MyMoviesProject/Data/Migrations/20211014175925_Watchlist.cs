namespace MyMoviesProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Watchlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WatchlistId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieWatchlist",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    WatchlistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWatchlist", x => new { x.MoviesId, x.WatchlistsId });
                    table.ForeignKey(
                        name: "FK_MovieWatchlist_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWatchlist_Watchlist_WatchlistsId",
                        column: x => x.WatchlistsId,
                        principalTable: "Watchlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WatchlistId",
                table: "AspNetUsers",
                column: "WatchlistId",
                unique: true,
                filter: "[WatchlistId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWatchlist_WatchlistsId",
                table: "MovieWatchlist",
                column: "WatchlistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Watchlist_WatchlistId",
                table: "AspNetUsers",
                column: "WatchlistId",
                principalTable: "Watchlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Watchlist_WatchlistId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MovieWatchlist");

            migrationBuilder.DropTable(
                name: "Watchlist");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WatchlistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WatchlistId",
                table: "AspNetUsers");
        }
    }
}
