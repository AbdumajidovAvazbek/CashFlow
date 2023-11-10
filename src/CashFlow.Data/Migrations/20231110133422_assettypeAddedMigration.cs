using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class assettypeAddedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_Users_UserId",
                table: "UserAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets");

            migrationBuilder.RenameTable(
                name: "UserAssets",
                newName: "Assets");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssets_UserId",
                table: "Assets",
                newName: "IX_Assets_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Assets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Assets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Assets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_UserId",
                table: "Assets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_UserId",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "UserAssets");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_UserId",
                table: "UserAssets",
                newName: "IX_UserAssets_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserAssets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_Users_UserId",
                table: "UserAssets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
