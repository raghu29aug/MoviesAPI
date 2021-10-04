using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesAPI.Migrations
{
    public partial class InitialMigratioin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RunningTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenredId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Comedy" },
                    { 2, "Romance" },
                    { 3, "Drama" },
                    { 4, "Action" },
                    { 5, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "RunningTime", "Title", "Year" },
                values: new object[,]
                {
                    { 7, 170, "Avatar", 2010 },
                    { 6, 180, "Matrix", 2005 },
                    { 5, 150, "Hangover", 1994 },
                    { 4, 200, "Titanic", 1998 },
                    { 3, 180, "Terminator", 1998 },
                    { 2, 120, "Avengers Endgame", 2018 },
                    { 1, 130, "Die Hard", 2000 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Smith" },
                    { 2, "Ron", "Swanson" },
                    { 3, "Michael", "Scott" },
                    { 4, "Ross", "Geller" },
                    { 5, "Jack", "Ryan" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "Id", "GenreId", "GenredId", "MovieId" },
                values: new object[,]
                {
                    { 1, null, 4, 1 },
                    { 15, null, 5, 7 },
                    { 14, null, 4, 6 },
                    { 13, null, 4, 5 },
                    { 11, null, 1, 5 },
                    { 10, null, 4, 4 },
                    { 9, null, 3, 4 },
                    { 12, null, 3, 5 },
                    { 7, null, 5, 3 },
                    { 6, null, 4, 3 },
                    { 5, null, 5, 2 },
                    { 4, null, 4, 2 },
                    { 3, null, 3, 2 },
                    { 2, null, 5, 1 },
                    { 8, null, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { 12, 5, 3, 5 },
                    { 19, 5, 5, 5 },
                    { 18, 3, 5, 5 },
                    { 17, 2, 5, 3 },
                    { 16, 1, 5, 5 },
                    { 15, 6, 4, 1 },
                    { 14, 5, 4, 3 },
                    { 13, 4, 4, 5 },
                    { 11, 4, 3, 4 },
                    { 3, 3, 1, 3 },
                    { 9, 6, 2, 1 },
                    { 8, 5, 2, 3 },
                    { 6, 4, 2, 3 },
                    { 5, 3, 2, 4 },
                    { 4, 2, 2, 2 },
                    { 20, 6, 5, 4 },
                    { 2, 2, 1, 5 },
                    { 1, 1, 1, 4 },
                    { 10, 3, 3, 3 },
                    { 21, 7, 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId",
                table: "MovieGenres",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
