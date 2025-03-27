using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorMenteeServer.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_UserId1",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_UserId2",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId2",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "UserId2",
                table: "Friendships",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Friendships",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_UserId1",
                table: "Friendships",
                newName: "IX_Friendships_SenderId");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendships",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_SenderId",
                table: "Friendships",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_SenderId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Friendships",
                newName: "UserId2");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Friendships",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_SenderId",
                table: "Friendships",
                newName: "IX_Friendships_UserId1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Friendships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId2",
                table: "Friendships",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_UserId1",
                table: "Friendships",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_UserId2",
                table: "Friendships",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
