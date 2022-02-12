using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterServer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowUserTb",
                columns: table => new
                {
                    TwitterUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUserTb", x => new { x.FollowerUserId, x.TwitterUserId });
                });

            migrationBuilder.CreateTable(
                name: "RetweetTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TwitteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RetweetDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetweetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetTb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TwitterUserTb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetweetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterUserTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwitterUserTb_RetweetTb_RetweetId",
                        column: x => x.RetweetId,
                        principalTable: "RetweetTb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TwitteTb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TwitteText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TwitterUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitteTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwitteTb_TwitterUserTb_TwitterUserId",
                        column: x => x.TwitterUserId,
                        principalTable: "TwitterUserTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUserTb_TwitterUserId",
                table: "FollowUserTb",
                column: "TwitterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetTb_TwitteId",
                table: "RetweetTb",
                column: "TwitteId");

            migrationBuilder.CreateIndex(
                name: "IX_TwitterUserTb_RetweetId",
                table: "TwitterUserTb",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TwitteTb_TwitterUserId",
                table: "TwitteTb",
                column: "TwitterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUserTb_TwitterUserTb_FollowerUserId",
                table: "FollowUserTb",
                column: "FollowerUserId",
                principalTable: "TwitterUserTb",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUserTb_TwitterUserTb_TwitterUserId",
                table: "FollowUserTb",
                column: "TwitterUserId",
                principalTable: "TwitterUserTb",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetweetTb_TwitteTb_TwitteId",
                table: "RetweetTb",
                column: "TwitteId",
                principalTable: "TwitteTb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwitteTb_TwitterUserTb_TwitterUserId",
                table: "TwitteTb");

            migrationBuilder.DropTable(
                name: "FollowUserTb");

            migrationBuilder.DropTable(
                name: "TwitterUserTb");

            migrationBuilder.DropTable(
                name: "RetweetTb");

            migrationBuilder.DropTable(
                name: "TwitteTb");
        }
    }
}
