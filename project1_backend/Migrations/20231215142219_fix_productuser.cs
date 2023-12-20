using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project1_backend.Migrations
{
    /// <inheritdoc />
    public partial class fix_productuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "PRODUCT_USER",
                nullable: false,
                oldNullable: true);
            migrationBuilder.DropPrimaryKey(
                name: "PK__PRODUCT___64227F3B363158BE",
                table: "PRODUCT_USER");


            migrationBuilder.AddPrimaryKey(
                name: "PK__PRODUCT___64227F3B363158BE",
                table: "PRODUCT_USER",
                columns: new[] { "Productid", "Userphonenumber", "Time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
