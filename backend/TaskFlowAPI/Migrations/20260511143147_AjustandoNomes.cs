using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoNomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_projetoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Projetos_Projetoid",
                table: "UserProjetos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Users_Userid",
                table: "UserProjetos");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserProjetos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "UserProjetos",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Projetoid",
                table: "UserProjetos",
                newName: "ProjetoId");

            migrationBuilder.RenameColumn(
                name: "projeto_id",
                table: "UserProjetos",
                newName: "Projeto_Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserProjetos",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjetos_Userid",
                table: "UserProjetos",
                newName: "IX_UserProjetos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjetos_Projetoid",
                table: "UserProjetos",
                newName: "IX_UserProjetos_ProjetoId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Tarefas",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "projetoId",
                table: "Tarefas",
                newName: "ProjetoId");

            migrationBuilder.RenameColumn(
                name: "prioridade",
                table: "Tarefas",
                newName: "Prioridade");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tarefas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Tarefas",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tarefas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_projetoId",
                table: "Tarefas",
                newName: "IX_Tarefas_ProjetoId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Projetos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Projetos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "Projetos",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Projetos",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Projetos_ProjetoId",
                table: "UserProjetos",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Users_UserId",
                table: "UserProjetos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Projetos_ProjetoId",
                table: "UserProjetos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Users_UserId",
                table: "UserProjetos");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProjetos",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "UserProjetos",
                newName: "Projetoid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserProjetos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Projeto_Id",
                table: "UserProjetos",
                newName: "projeto_id");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "UserProjetos",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjetos_UserId",
                table: "UserProjetos",
                newName: "IX_UserProjetos_Userid");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjetos_ProjetoId",
                table: "UserProjetos",
                newName: "IX_UserProjetos_Projetoid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tarefas",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Tarefas",
                newName: "projetoId");

            migrationBuilder.RenameColumn(
                name: "Prioridade",
                table: "Tarefas",
                newName: "prioridade");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tarefas",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tarefas",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tarefas",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_ProjetoId",
                table: "Tarefas",
                newName: "IX_Tarefas_projetoId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projetos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projetos",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Projetos",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projetos",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_projetoId",
                table: "Tarefas",
                column: "projetoId",
                principalTable: "Projetos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Projetos_Projetoid",
                table: "UserProjetos",
                column: "Projetoid",
                principalTable: "Projetos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Users_Userid",
                table: "UserProjetos",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
