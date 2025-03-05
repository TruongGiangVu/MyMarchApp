using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarchApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DB_USER",
                columns: table => new
                {
                    USER_ID = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    USER_NAME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ROLE = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    PASSWORD = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DB_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "TODO_ITEM",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    NAME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    RATE = table.Column<decimal>(type: "NUMBER", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    IS_DONE = table.Column<string>(type: "VARCHAR2(5)", nullable: false),
                    PRIORITY = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    CREATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    UPDATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    UPDATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO_ITEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TODO_TAG",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TEXT = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    COLOR = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CREATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    CREATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    UPDATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    UPDATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO_TAG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TODO_CHECKLIST",
                columns: table => new
                {
                    ID = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TEXT = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IS_DONE = table.Column<string>(type: "VARCHAR2(5)", nullable: false),
                    ITEM_ID = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    CREATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    CREATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    UPDATED_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    UPDATED_BY = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO_CHECKLIST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TODO_CHECKLIST_TODO_ITEM_ITEM_ID",
                        column: x => x.ITEM_ID,
                        principalTable: "TODO_ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TODO_TAG_ITEM",
                columns: table => new
                {
                    ITEM_ID = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TAG_ID = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO_TAG_ITEM", x => new { x.ITEM_ID, x.TAG_ID });
                    table.ForeignKey(
                        name: "FK_TODO_TAG_ITEM_TODO_ITEM_ITEM_ID",
                        column: x => x.ITEM_ID,
                        principalTable: "TODO_ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TODO_TAG_ITEM_TODO_TAG_TAG_ID",
                        column: x => x.TAG_ID,
                        principalTable: "TODO_TAG",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TODO_CHECKLIST_ITEM_ID",
                table: "TODO_CHECKLIST",
                column: "ITEM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TODO_TAG_ITEM_TAG_ID",
                table: "TODO_TAG_ITEM",
                column: "TAG_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DB_USER");

            migrationBuilder.DropTable(
                name: "TODO_CHECKLIST");

            migrationBuilder.DropTable(
                name: "TODO_TAG_ITEM");

            migrationBuilder.DropTable(
                name: "TODO_ITEM");

            migrationBuilder.DropTable(
                name: "TODO_TAG");
        }
    }
}
