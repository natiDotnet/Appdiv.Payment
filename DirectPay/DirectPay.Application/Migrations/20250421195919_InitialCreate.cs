using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectPay.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DirectPay");

            migrationBuilder.CreateTable(
                name: "settings",
                schema: "DirectPay",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    configuration = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transations",
                schema: "DirectPay",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    tx_ref = table.Column<string>(type: "text", nullable: true),
                    callback_url = table.Column<string>(type: "text", nullable: true),
                    return_url = table.Column<string>(type: "text", nullable: true),
                    payment_status = table.Column<bool>(type: "boolean", nullable: false),
                    reference = table.Column<string>(type: "text", nullable: true),
                    payment_type = table.Column<string>(type: "text", nullable: true),
                    payment_method = table.Column<string>(type: "text", nullable: true),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    customization = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transations", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings",
                schema: "DirectPay");

            migrationBuilder.DropTable(
                name: "transations",
                schema: "DirectPay");
        }
    }
}
