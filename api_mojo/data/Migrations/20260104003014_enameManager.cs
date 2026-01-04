using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_mojo.data.migrations
{
    /// <inheritdoc />
    public partial class enameManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrats_Users_UserIdRH",
                table: "Contrats");

            migrationBuilder.RenameColumn(
                name: "UserIdRH",
                table: "Contrats",
                newName: "UserRhId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrats_UserIdRH",
                table: "Contrats",
                newName: "IX_Contrats_UserRhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrats_Users_UserRhId",
                table: "Contrats",
                column: "UserRhId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrats_Users_UserRhId",
                table: "Contrats");

            migrationBuilder.RenameColumn(
                name: "UserRhId",
                table: "Contrats",
                newName: "UserIdRH");

            migrationBuilder.RenameIndex(
                name: "IX_Contrats_UserRhId",
                table: "Contrats",
                newName: "IX_Contrats_UserIdRH");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrats_Users_UserIdRH",
                table: "Contrats",
                column: "UserIdRH",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
