using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Examples.Migrations
{
    public partial class AddingBloggersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Bloggers",
                table => new
                {
                    BloggerId = table.Column<int>("int")
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>("int"),
                    Firstname = table.Column<string>("nvarchar(max)", nullable: true),
                    Surname = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Bloggers", x => x.BloggerId); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Bloggers");
        }
    }
}