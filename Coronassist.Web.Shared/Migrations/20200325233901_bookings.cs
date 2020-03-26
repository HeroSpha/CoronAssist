using Microsoft.EntityFrameworkCore.Migrations;

namespace Coronassist.Web.Shared.Migrations
{
    public partial class bookings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_PatientId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_PatientId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AccountBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Settings",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Books",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_Id",
                table: "Books",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_Id",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountBookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PatientId",
                table: "Books",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_PatientId",
                table: "Books",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
