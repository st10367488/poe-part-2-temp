using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddCostZarToServiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostZAR",
                table: "ServiceRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostZAR",
                table: "ServiceRequests");
        }
    }
}
