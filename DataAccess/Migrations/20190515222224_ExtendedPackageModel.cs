using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ExtendedPackageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Packages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Packages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Packages",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unity",
                table: "Packages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Packages",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PackageDependencyModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Package = table.Column<string>(maxLength: 150, nullable: false),
                    Version = table.Column<string>(nullable: true),
                    PackageModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDependencyModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageDependencyModel_Packages_PackageModelId",
                        column: x => x.PackageModelId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageDependencyModel_PackageModelId",
                table: "PackageDependencyModel",
                column: "PackageModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageDependencyModel");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Unity",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Packages",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
