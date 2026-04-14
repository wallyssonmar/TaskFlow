using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixUserProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_UserProjetos_UserProjetoid",
                table: "Projetos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProjetos_UserProjetoid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserProjetoid",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjetos",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_UserProjetoid",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "UserProjetoid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserProjetoid",
                table: "Projetos");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "projeto_id",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Projetoid",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjetos",
                table: "UserProjetos",
                columns: new[] { "user_id", "projeto_id" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjetos_Projetoid",
                table: "UserProjetos",
                column: "Projetoid");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjetos_Userid",
                table: "UserProjetos",
                column: "Userid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Projetos_Projetoid",
                table: "UserProjetos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjetos_Users_Userid",
                table: "UserProjetos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjetos",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_UserProjetos_Projetoid",
                table: "UserProjetos");

            migrationBuilder.DropIndex(
                name: "IX_UserProjetos_Userid",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "projeto_id",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "Projetoid",
                table: "UserProjetos");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "UserProjetos");

            migrationBuilder.AddColumn<int>(
                name: "UserProjetoid",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "UserProjetos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserProjetoid",
                table: "Projetos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjetos",
                table: "UserProjetos",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserProjetoid",
                table: "Users",
                column: "UserProjetoid");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_UserProjetoid",
                table: "Projetos",
                column: "UserProjetoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_UserProjetos_UserProjetoid",
                table: "Projetos",
                column: "UserProjetoid",
                principalTable: "UserProjetos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProjetos_UserProjetoid",
                table: "Users",
                column: "UserProjetoid",
                principalTable: "UserProjetos",
                principalColumn: "id");
        }
    }
}
