using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixOneToManyCascadeDepsAndVersionIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("PackageDependencyModel", "PackageModelId", new object[] {null});
            migrationBuilder.DropForeignKey(
                name: "FK_PackageDependencyModel_Packages_PackageModelId",
                table: "PackageDependencyModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageVersionModel_Packages_PackageModelId",
                table: "PackageVersionModel");

            migrationBuilder.AlterColumn<int>(
                name: "PackageModelId",
                table: "PackageVersionModel",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PackageModelId",
                table: "PackageDependencyModel",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageDependencyModel_Packages_PackageModelId",
                table: "PackageDependencyModel",
                column: "PackageModelId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageVersionModel_Packages_PackageModelId",
                table: "PackageVersionModel",
                column: "PackageModelId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageDependencyModel_Packages_PackageModelId",
                table: "PackageDependencyModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageVersionModel_Packages_PackageModelId",
                table: "PackageVersionModel");

            migrationBuilder.AlterColumn<int>(
                name: "PackageModelId",
                table: "PackageVersionModel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PackageModelId",
                table: "PackageDependencyModel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PackageDependencyModel_Packages_PackageModelId",
                table: "PackageDependencyModel",
                column: "PackageModelId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageVersionModel_Packages_PackageModelId",
                table: "PackageVersionModel",
                column: "PackageModelId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
