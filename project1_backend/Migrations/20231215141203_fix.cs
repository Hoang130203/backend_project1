using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project1_backend.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT_USER",
                table: "PRODUCT_USER",
                columns: new[] { "Productid", "Userphonenumber", "Time" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT_USER",
                table: "PRODUCT_USER");
        }
    }
}
