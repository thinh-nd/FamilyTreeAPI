using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.API.Migrations
{
    public partial class Remove_Person_MiddleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Person",
                type: "varchar(100)",
                nullable: true);
        }
    }
}
