using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessOnline.Data.Migrations
{
    public partial class addedSubscriptionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "SubscriptionType",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "SubscriptionType");
        }
    }
}
