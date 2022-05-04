using Microsoft.EntityFrameworkCore.Migrations;

namespace project1.Data.datamodel
{
    public partial class alter_model2_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VocabID",
                table: "model2data",
                newName: "Model1Key");

            migrationBuilder.CreateIndex(
                name: "IX_model2data_Model1Key",
                table: "model2data",
                column: "Model1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_model2data_model1data_Model1Key",
                table: "model2data",
                column: "Model1Key",
                principalTable: "model1data",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_model2data_model1data_Model1Key",
                table: "model2data");

            migrationBuilder.DropIndex(
                name: "IX_model2data_Model1Key",
                table: "model2data");

            migrationBuilder.RenameColumn(
                name: "Model1Key",
                table: "model2data",
                newName: "VocabID");
        }
    }
}
