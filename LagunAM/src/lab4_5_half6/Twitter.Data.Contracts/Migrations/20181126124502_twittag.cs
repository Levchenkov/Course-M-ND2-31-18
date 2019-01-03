using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Twitter.Data.Contracts.Migrations
{
    public partial class twittag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwitTag_Tags_TagId",
                table: "TwitTag");

            migrationBuilder.DropForeignKey(
                name: "FK_TwitTag_Twits_TwitId",
                table: "TwitTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TwitTag",
                table: "TwitTag");

            migrationBuilder.RenameTable(
                name: "TwitTag",
                newName: "TwitTags");

            migrationBuilder.RenameIndex(
                name: "IX_TwitTag_TagId",
                table: "TwitTags",
                newName: "IX_TwitTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TwitTags",
                table: "TwitTags",
                columns: new[] { "TwitId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TwitTags_Tags_TagId",
                table: "TwitTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TwitTags_Twits_TwitId",
                table: "TwitTags",
                column: "TwitId",
                principalTable: "Twits",
                principalColumn: "TwitId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwitTags_Tags_TagId",
                table: "TwitTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TwitTags_Twits_TwitId",
                table: "TwitTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TwitTags",
                table: "TwitTags");

            migrationBuilder.RenameTable(
                name: "TwitTags",
                newName: "TwitTag");

            migrationBuilder.RenameIndex(
                name: "IX_TwitTags_TagId",
                table: "TwitTag",
                newName: "IX_TwitTag_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TwitTag",
                table: "TwitTag",
                columns: new[] { "TwitId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TwitTag_Tags_TagId",
                table: "TwitTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TwitTag_Twits_TwitId",
                table: "TwitTag",
                column: "TwitId",
                principalTable: "Twits",
                principalColumn: "TwitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
