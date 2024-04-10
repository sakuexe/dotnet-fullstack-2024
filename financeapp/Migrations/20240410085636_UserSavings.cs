using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financeapp.Migrations
{
    /// <inheritdoc />
    public partial class UserSavings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SavingsGoal",
                table: "Users",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavingsGoal",
                table: "Users");
        }
    }
}
