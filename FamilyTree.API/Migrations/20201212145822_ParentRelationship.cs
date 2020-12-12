using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.API.Migrations
{
    public partial class ParentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentChildRelationship");

            migrationBuilder.CreateTable(
                name: "ChildRelationship",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildRelationship", x => x.RelationId);
                    table.CheckConstraint("CK_ChildParadox", "PersonId != ChildId");
                    table.ForeignKey(
                        name: "FK_ChildRelationship_Person_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildRelationship_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentRelationship",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentRelationship", x => x.RelationId);
                    table.CheckConstraint("CK_ParentParadox", "PersonId != ParentId");
                    table.ForeignKey(
                        name: "FK_ParentRelationship_Person_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentRelationship_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildRelationship_ChildId",
                table: "ChildRelationship",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChildRelationship_PersonId",
                table: "ChildRelationship",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentRelationship_ParentId",
                table: "ParentRelationship",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentRelationship_PersonId",
                table: "ParentRelationship",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildRelationship");

            migrationBuilder.DropTable(
                name: "ParentRelationship");

            migrationBuilder.CreateTable(
                name: "ParentChildRelationship",
                columns: table => new
                {
                    RelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ParentChildRelationship_ChildId",
                table: "ParentChildRelationship",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentChildRelationship_ParentId",
                table: "ParentChildRelationship",
                column: "ParentId");
        }
    }
}
