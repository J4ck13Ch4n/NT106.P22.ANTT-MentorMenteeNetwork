using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorMenteeServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGoalForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Goals_MenteeId",
                table: "Goals",
                column: "MenteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MentorId",
                table: "Goals",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_MenteeId",
                table: "Goals",
                column: "MenteeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_MentorId",
                table: "Goals",
                column: "MentorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_MenteeId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_MentorId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_MenteeId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_MentorId",
                table: "Goals");
        }
    }
}
