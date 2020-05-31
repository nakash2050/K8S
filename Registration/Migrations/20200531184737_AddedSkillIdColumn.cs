using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration.Migrations
{
    public partial class AddedSkillIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "tblEmployees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "tblEmployees");
        }
    }
}
