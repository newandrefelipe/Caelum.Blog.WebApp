using Microsoft.EntityFrameworkCore.Migrations;

namespace Caelum.Blog.WebApp.Dados.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Resumo = table.Column<string>(nullable: true),
                    Categoria = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new string[] { "Titulo", "Resumo", "Categoria" },
                values: new object[] {"Harry Potter I", "Pedra Filosofal", "Livros" }
            );

            
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new string[] { "Titulo", "Resumo", "Categoria" },
                values: new object[] { "Harry Potter II", "Camara Secreta", "Livros" }
            );

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new string[] { "Titulo", "Resumo", "Categoria" },
                values: new object[] { "Harry Potter III", "Prisioneiro de Askaban", "Livros" }
            );

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new string[] { "Titulo", "Resumo", "Categoria" },
                values: new object[] { "Harry Potter IV", "Cálice de Fogo", "Livros" }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
