using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataCore.Migrations
{
    public partial class AddedMoviePictureIDColumnToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoviePictureID",
                table: "Posts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8585056b-4d41-42fc-8d40-0efeac91f65d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8bfa92fb-c042-479e-8afe-64f0a3fb4cb3");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                columns: new[] { "MoviePictureID", "PostDate" },
                values: new object[] { "1", new DateTime(2019, 3, 30, 14, 43, 23, 719, DateTimeKind.Local).AddTicks(3763) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviePictureID",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e3f32bfe-2431-415b-b628-cdc54ec03465");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "50f742ca-0b9d-4e59-90b8-514bc423d530");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                column: "PostDate",
                value: new DateTime(2019, 3, 30, 14, 31, 5, 532, DateTimeKind.Local).AddTicks(1393));
        }
    }
}
