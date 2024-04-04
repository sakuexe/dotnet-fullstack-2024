using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financeapp.Migrations
{
    /// <inheritdoc />
    public partial class Renaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_User_UserId",
                table: "Finance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Finance",
                table: "Finance");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Finance",
                newName: "Finances");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_UserId",
                table: "Finances",
                newName: "IX_Finances_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Finances",
                table: "Finances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Finances_Users_UserId",
                table: "Finances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finances_Users_UserId",
                table: "Finances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Finances",
                table: "Finances");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Finances",
                newName: "Finance");

            migrationBuilder.RenameIndex(
                name: "IX_Finances_UserId",
                table: "Finance",
                newName: "IX_Finance_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Finance",
                table: "Finance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_User_UserId",
                table: "Finance",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
