using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataCore.Migrations
{
    public partial class AddedHTTPUrlColumnToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoviePictureURL",
                table: "Posts",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviePictureURL",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8656131e-7485-4cbc-9376-6cf2ce6e6042");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7b7579ea-4cc7-4d35-bf8b-39e9e8121f79");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                column: "PostDate",
                value: new DateTime(2019, 3, 17, 13, 41, 31, 54, DateTimeKind.Local).AddTicks(5000));
        }
    }
}
