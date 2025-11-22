using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistant.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonTaskItemGroupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTaskItemGroup_Persons_MembersId",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTaskItemGroup_TaskItemGroups_TaskGroupsId",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTaskItemGroup",
                table: "PersonTaskItemGroup");

            migrationBuilder.RenameColumn(
                name: "TaskGroupsId",
                table: "PersonTaskItemGroup",
                newName: "TaskItemGroupId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "PersonTaskItemGroup",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTaskItemGroup_TaskGroupsId",
                table: "PersonTaskItemGroup",
                newName: "IX_PersonTaskItemGroup_TaskItemGroupId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersonTaskItemGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PersonTaskItemGroup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PersonTaskItemGroup",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PersonTaskItemGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTaskItemGroup",
                table: "PersonTaskItemGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTaskItemGroup_PersonId",
                table: "PersonTaskItemGroup",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTaskItemGroup_Persons_PersonId",
                table: "PersonTaskItemGroup",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTaskItemGroup_TaskItemGroups_TaskItemGroupId",
                table: "PersonTaskItemGroup",
                column: "TaskItemGroupId",
                principalTable: "TaskItemGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTaskItemGroup_Persons_PersonId",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonTaskItemGroup_TaskItemGroups_TaskItemGroupId",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTaskItemGroup",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropIndex(
                name: "IX_PersonTaskItemGroup_PersonId",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PersonTaskItemGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PersonTaskItemGroup");

            migrationBuilder.RenameColumn(
                name: "TaskItemGroupId",
                table: "PersonTaskItemGroup",
                newName: "TaskGroupsId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "PersonTaskItemGroup",
                newName: "MembersId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTaskItemGroup_TaskItemGroupId",
                table: "PersonTaskItemGroup",
                newName: "IX_PersonTaskItemGroup_TaskGroupsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTaskItemGroup",
                table: "PersonTaskItemGroup",
                columns: new[] { "MembersId", "TaskGroupsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTaskItemGroup_Persons_MembersId",
                table: "PersonTaskItemGroup",
                column: "MembersId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTaskItemGroup_TaskItemGroups_TaskGroupsId",
                table: "PersonTaskItemGroup",
                column: "TaskGroupsId",
                principalTable: "TaskItemGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
