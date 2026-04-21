using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractFlow.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Contracts_ContractId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Contracts_ContractId",
                table: "ServiceRequests",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Contracts_ContractId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Contracts_ContractId",
                table: "ServiceRequests",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
