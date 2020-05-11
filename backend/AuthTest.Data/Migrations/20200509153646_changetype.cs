using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthTest.Data.Migrations
{
    public partial class changetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AnswerValue",
                table: "Answers",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AnswerValue",
                table: "Answers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
