using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Groupchat_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageCodeToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageCode",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageCode",
                table: "Messages");
        }
    }
}
