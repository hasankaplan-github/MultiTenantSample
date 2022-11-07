using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantSample.Infra.Db.SampleDbContext.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "some_tenant_data_class",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    my_property = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_some_tenant_data_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "some_tenant_data_class");

            migrationBuilder.DropTable(
                name: "tenant");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
