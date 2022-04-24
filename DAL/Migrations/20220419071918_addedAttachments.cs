using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addedAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Medias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Medias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_CommentId",
                table: "Medias",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TicketId",
                table: "Medias",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Comments_CommentId",
                table: "Medias",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Tickets_TicketId",
                table: "Medias",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Comments_CommentId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Tickets_TicketId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_CommentId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TicketId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Medias");
        }
    }
}
