using Microsoft.EntityFrameworkCore.Migrations;

namespace Coronassist.Web.Shared.Migrations
{
    public partial class books : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_Id",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingType",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultationFee",
                table: "Books",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationFee = table.Column<decimal>(nullable: false),
                    OpenTime = table.Column<string>(nullable: true),
                    ClosingTime = table.Column<string>(nullable: true),
                    OpenWeekend = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_PatientId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Books_PatientId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookingType",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_Id",
                table: "Books",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
