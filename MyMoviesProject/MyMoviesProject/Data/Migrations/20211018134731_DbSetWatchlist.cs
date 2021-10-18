using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMoviesProject.Data.Migrations
{
    public partial class DbSetWatchlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Watchlist_WatchlistId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieWatchlist_Watchlist_WatchlistsId",
                table: "MovieWatchlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlist",
                table: "Watchlist");

            migrationBuilder.RenameTable(
                name: "Watchlist",
                newName: "Watchlists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Watchlists_WatchlistId",
                table: "AspNetUsers",
                column: "WatchlistId",
                principalTable: "Watchlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieWatchlist_Watchlists_WatchlistsId",
                table: "MovieWatchlist",
                column: "WatchlistsId",
                principalTable: "Watchlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Watchlists_WatchlistId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieWatchlist_Watchlists_WatchlistsId",
                table: "MovieWatchlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists");

            migrationBuilder.RenameTable(
                name: "Watchlists",
                newName: "Watchlist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlist",
                table: "Watchlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Watchlist_WatchlistId",
                table: "AspNetUsers",
                column: "WatchlistId",
                principalTable: "Watchlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieWatchlist_Watchlist_WatchlistsId",
                table: "MovieWatchlist",
                column: "WatchlistsId",
                principalTable: "Watchlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
