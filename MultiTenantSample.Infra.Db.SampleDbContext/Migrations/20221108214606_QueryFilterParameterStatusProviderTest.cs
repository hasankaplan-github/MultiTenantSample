using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantSample.Infra.Db.SampleDbContext.Migrations
{
    public partial class QueryFilterParameterStatusProviderTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "some_tenant_data_class",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "some_tenant_data_class");
        }
    }
}
