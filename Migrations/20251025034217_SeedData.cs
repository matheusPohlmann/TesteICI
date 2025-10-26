using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTesteICI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Nome", "Senha", "Email" },
                values: new object[,]
                {
                    { 1, "Admin User", "senha123", "admin@teste.com" },
                    { 2, "Matheus Pohlmann", "senha123", "matheus@teste.com" },
                    { 3, "Redator Chefe", "senha123", "chefe@teste.com" },
                    { 4, "Redator Júnior", "senha123", "junior@teste.com" },
                    { 5, "Analista Tech", "senha123", "analista@teste.com" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Tecnologia" },
                    { 2, "Esportes" },
                    { 3, "Política" },
                    { 4, "Local" },
                    { 5, "Internacional" }
                });

            migrationBuilder.InsertData(
                table: "Noticias",
                columns: new[] { "Id", "Titulo", "Texto", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Lançamento do .NET 9", "O .NET 9 promete performance aprimorada e novos recursos e frameworks...", 1 },
                    { 2, "Finais do Campeonato Brasileiro", "O time A venceu o time B na final emocionante, com placar apertado de 3x2.", 2 },
                    { 3, "Mudanças na Lei de Dados Pessoais", "Novas emendas na lei X causam debate entre especialistas em privacidade.", 3 },
                    { 4, "Novo Projeto de Mobilidade Urbana", "A prefeitura anuncia um novo sistema de bicicletas compartilhadas no centro da cidade.", 4 },
                    { 5, "Cúpula Econômica Global", "Líderes se reúnem para discutir a crise de energia e as taxas de inflação.", 5 }
                });

            migrationBuilder.InsertData(
                table: "NoticiaTags",
                columns: new[] { "Id", "NoticiaId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 1 }, 
                    { 2, 1, 5 }, 
                    
                    { 3, 2, 2 }, 
                    { 4, 2, 4 }, 

                    { 5, 3, 3 }, 
                    { 6, 3, 1 }, 

                    { 7, 4, 4 }, 
                    
                    { 8, 5, 5 }, 
                    { 9, 5, 3 }, 
                    { 10, 5, 1 } 
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "NoticiaTags", keyColumn: "Id", keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            migrationBuilder.DeleteData(table: "Noticias", keyColumn: "Id", keyValues: new object[] { 1, 2, 3, 4, 5 });
            migrationBuilder.DeleteData(table: "Tags", keyColumn: "Id", keyValues: new object[] { 1, 2, 3, 4, 5 });
            migrationBuilder.DeleteData(table: "Usuarios", keyColumn: "Id", keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}
