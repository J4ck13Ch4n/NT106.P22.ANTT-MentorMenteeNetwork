using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorMenteeServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipToUserFriend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_MenteeId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_MentorId",
                table: "Relationships");

            migrationBuilder.DropCheckConstraint(
                name: "chk_self_relationship",
                table: "Relationships");

            migrationBuilder.RenameColumn(
                name: "MentorId",
                table: "Relationships",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "MenteeId",
                table: "Relationships",
                newName: "FriendId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_MentorId",
                table: "Relationships",
                newName: "IX_Relationships_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_MenteeId",
                table: "Relationships",
                newName: "IX_Relationships_FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_FriendId",
                table: "Relationships",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_FriendId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Relationships",
                newName: "MentorId");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Relationships",
                newName: "MenteeId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_UserId",
                table: "Relationships",
                newName: "IX_Relationships_MentorId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_FriendId",
                table: "Relationships",
                newName: "IX_Relationships_MenteeId");

            migrationBuilder.AddCheckConstraint(
                name: "chk_self_relationship",
                table: "Relationships",
                sql: "[MentorId] <> [MenteeId]");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_MenteeId",
                table: "Relationships",
                column: "MenteeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_MentorId",
                table: "Relationships",
                column: "MentorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
