using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_filters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    FilterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFilter_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilter_Filter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilterValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilterId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterValue_Filter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertId = table.Column<int>(type: "integer", nullable: false),
                    FilterValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertValue_Advert_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Advert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertValue_FilterValue_FilterValueId",
                        column: x => x.FilterValueId,
                        principalTable: "FilterValue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertValue_AdvertId",
                table: "AdvertValue",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertValue_FilterValueId",
                table: "AdvertValue",
                column: "FilterValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilter_CategoryId",
                table: "CategoryFilter",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilter_FilterId",
                table: "CategoryFilter",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterValue_FilterId",
                table: "FilterValue",
                column: "FilterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertValue");

            migrationBuilder.DropTable(
                name: "CategoryFilter");

            migrationBuilder.DropTable(
                name: "FilterValue");

            migrationBuilder.DropTable(
                name: "Filter");
        }
    }
}
