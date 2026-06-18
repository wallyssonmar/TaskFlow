using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustarUserProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Projetos_ProjetoId",
                table: "UserProjetos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Users_UserId",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_UserProjetos_ProjetoId",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_UserProjetos_UserId",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProjetos");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjetos_Projeto_Id",
                table: "UserProjetos",
                column: "Projeto_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Projetos_Projeto_Id",
                table: "UserProjetos",
                column: "Projeto_Id",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjetos_Users_User_Id",
                table: "UserProjetos",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Projetos_Projeto_Id",
                table: "UserProjetos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Users_User_Id",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_UserProjetos_Projeto_Id",
                table: "UserProjetos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserProjetos_ProjetoId",
                table: "UserProjetos",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjetos_UserId",
                table: "UserProjetos",
                column: "UserId");

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
    }
}
