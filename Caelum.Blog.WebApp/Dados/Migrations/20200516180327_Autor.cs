using Microsoft.EntityFrameworkCore.Migrations;

namespace Caelum.Blog.WebApp.Dados.Migrations
{
    public partial class Autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAutor",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IdAutor",
                table: "Posts",
                column: "IdAutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Usuarios_IdAutor",
                table: "Posts",
                column: "IdAutor",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Usuarios_IdAutor",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_IdAutor",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IdAutor",
                table: "Posts");
        }
    }
}
