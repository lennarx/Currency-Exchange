using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualMind.Exchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CURRENCY_PURCHASE",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMOUNT_IN_PESOS = table.Column<double>(type: "float", nullable: false),
                    CURRENCY_EXCHANGE_RATE = table.Column<double>(type: "float", nullable: false),
                    ISO_CODE = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PURCHASE_DATE = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENCY_PURCHASE", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CURRENCY_PURCHASE");
        }
    }
}
