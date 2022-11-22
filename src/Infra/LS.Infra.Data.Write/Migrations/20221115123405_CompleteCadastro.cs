using Microsoft.EntityFrameworkCore.Migrations;

namespace LS.Infra.Data.Write.Migrations
{
    public partial class CompleteCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Clientes",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profissao",
                table: "Clientes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Profissao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Clientes");
        }
    }
}
