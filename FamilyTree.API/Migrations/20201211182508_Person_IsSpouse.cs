using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.API.Migrations
{
    public partial class Person_IsSpouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpouse",
                table: "Person",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpouse",
                table: "Person");
        }
    }
}
