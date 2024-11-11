using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "COUPONS",
                columns: new[] { "COUPONID", "COUPONCODE", "DISCOUNTAMOUNT", "MINAMOUNT" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.0, 20 },
                    { 2, "20OFF", 20.0, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "COUPONS",
                keyColumn: "COUPONID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "COUPONS",
                keyColumn: "COUPONID",
                keyValue: 2);
        }
    }
}
