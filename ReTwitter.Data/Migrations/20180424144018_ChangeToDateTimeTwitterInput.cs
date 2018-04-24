using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReTwitter.Data.Migrations
{
    public partial class ChangeToDateTimeTwitterInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OriginalTweetCreatedOn",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FolloweeOriginallyCreatedOn",
                table: "Followees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalTweetCreatedOn",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "FolloweeOriginallyCreatedOn",
                table: "Followees",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
