using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project1_backend.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationtimeofcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
               name: "Time",
               table: "sanbong_user",
               nullable: false);
            migrationBuilder.DropPrimaryKey(
               name: "PK_SANBONG_USER",
               table: "sanbong_user");
            migrationBuilder.AddPrimaryKey(
               name: "PK_SANBONG_USER",
               table: "sanbong_user",
               columns: new[] { "Fieldid", "Userphonenumber", "Time" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
