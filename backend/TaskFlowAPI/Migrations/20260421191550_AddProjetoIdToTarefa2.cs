using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProjetoIdToTarefa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_projeto_idid",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "projeto_idid",
                table: "Tarefas",
                newName: "projetoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_projeto_idid",
                table: "Tarefas",
                newName: "IX_Tarefas_projetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_projetoId",
                table: "Tarefas",
                column: "projetoId",
                principalTable: "Projetos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_projetoId",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "projetoId",
                table: "Tarefas",
                newName: "projeto_idid");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_projetoId",
                table: "Tarefas",
                newName: "IX_Tarefas_projeto_idid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_projeto_idid",
                table: "Tarefas",
                column: "projeto_idid",
                principalTable: "Projetos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
