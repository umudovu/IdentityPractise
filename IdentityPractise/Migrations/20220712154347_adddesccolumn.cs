using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityPractise.Migrations
{
    public partial class adddesccolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Blogs");
        }
    }
}
