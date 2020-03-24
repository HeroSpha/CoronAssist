using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coronassist.Web.Shared.Migrations
{
    public partial class time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
