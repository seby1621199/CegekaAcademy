using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Fundraisers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraiser_FundraiserId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Fundraiser_Persons_OwnerId",
                table: "Fundraiser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fundraiser",
                table: "Fundraiser");

            migrationBuilder.RenameTable(
                name: "Fundraiser",
                newName: "Fundraisers");

            migrationBuilder.RenameIndex(
                name: "IX_Fundraiser_OwnerId",
                table: "Fundraisers",
                newName: "IX_Fundraisers_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fundraisers",
                table: "Fundraisers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundraisers_Persons_OwnerId",
                table: "Fundraisers",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Fundraisers_FundraiserId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Fundraisers_Persons_OwnerId",
                table: "Fundraisers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fundraisers",
                table: "Fundraisers");

            migrationBuilder.RenameTable(
                name: "Fundraisers",
                newName: "Fundraiser");

            migrationBuilder.RenameIndex(
                name: "IX_Fundraisers_OwnerId",
                table: "Fundraiser",
                newName: "IX_Fundraiser_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fundraiser",
                table: "Fundraiser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraiser_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraiser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundraiser_Persons_OwnerId",
                table: "Fundraiser",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
