using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.API.Migrations
{
    public partial class Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentChildRelationship",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentChildRelationship", x => x.RelationId);
                    table.CheckConstraint("CK_Paradox", "ParentId != ChildId");
                    table.ForeignKey(
                        name: "FK_ParentChildRelationship_Person_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentChildRelationship_Person_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpousalRelationship",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpousalRelationship", x => x.RelationId);
                    table.CheckConstraint("CK_Monogamy", "PersonId != SpouseId");
                    table.ForeignKey(
                        name: "FK_SpousalRelationship_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpousalRelationship_Person_SpouseId",
                        column: x => x.SpouseId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentChildRelationship_ChildId",
                table: "ParentChildRelationship",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentChildRelationship_ParentId",
                table: "ParentChildRelationship",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpousalRelationship_PersonId",
                table: "SpousalRelationship",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpousalRelationship_SpouseId",
                table: "SpousalRelationship",
                column: "SpouseId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentChildRelationship");

            migrationBuilder.DropTable(
                name: "SpousalRelationship");
        }
    }
}
