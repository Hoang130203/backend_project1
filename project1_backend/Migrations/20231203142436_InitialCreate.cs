using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project1_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountUser",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUser", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Account = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Account);
                });

            migrationBuilder.CreateTable(
                name: "BANNER",
                columns: table => new
                {
                    BANNERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LINKIMG = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BANNER__0F8F784DAF66C86F", x => x.BANNERID);
                });

            migrationBuilder.CreateTable(
                name: "InfoProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linkimg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoProduct", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCTNAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DETAIL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    COLOR = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LINKIMG = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RATE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCT__34980AA2B87F5836", x => x.PRODUCTID);
                });

            migrationBuilder.CreateTable(
                name: "SANBONG",
                columns: table => new
                {
                    FIELDID = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    LINKIMG = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COST = table.Column<int>(type: "int", nullable: false),
                    RATE = table.Column<int>(type: "int", nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DECRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANBONG__707268028301F47D", x => x.FIELDID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    PHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BIRTHDATE = table.Column<DateTime>(type: "date", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AVT = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USERS__8F2B07B0110AA2A0", x => x.PHONENUMBER);
                });

            migrationBuilder.CreateTable(
                name: "KHOHANG",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHOHANG__34980AA23774C1AD", x => x.PRODUCTID);
                    table.ForeignKey(
                        name: "FK__KHOHANG__PRODUCT__3B75D760",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    PHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    PASSWORD = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ACCOUNT__8F2B07B04C9DA7A6", x => x.PHONENUMBER);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_USERS",
                        column: x => x.PHONENUMBER,
                        principalTable: "USERS",
                        principalColumn: "PHONENUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DONHANG",
                columns: table => new
                {
                    ORDERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    TOTALCOST = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TIME = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DONHANG__491E419249F835B4", x => x.ORDERID);
                    table.ForeignKey(
                        name: "DONHANGG_USER",
                        column: x => x.PHONENUMBER,
                        principalTable: "USERS",
                        principalColumn: "PHONENUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_USER",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "int", nullable: false),
                    USERPHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    RATE = table.Column<int>(type: "int", nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIME = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCT___64227F3B363158BE", x => new { x.PRODUCTID, x.USERPHONENUMBER });
                    table.ForeignKey(
                        name: "FK_3",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4",
                        column: x => x.USERPHONENUMBER,
                        principalTable: "USERS",
                        principalColumn: "PHONENUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanbong_user",
                columns: table => new
                {
                    FIELDID = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    USERPHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    RATE = table.Column<int>(type: "int", nullable: true),
                    COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANBONG_USER", x => new { x.FIELDID, x.USERPHONENUMBER });
                    table.ForeignKey(
                        name: "FK_1",
                        column: x => x.FIELDID,
                        principalTable: "SANBONG",
                        principalColumn: "FIELDID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_2",
                        column: x => x.USERPHONENUMBER,
                        principalTable: "USERS",
                        principalColumn: "PHONENUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SANPHAM_GIOHANG",
                columns: table => new
                {
                    USERPHONENUMBER = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    PRODUCTID = table.Column<int>(type: "int", nullable: false),
                    COLOR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANPHAM___38EED93EB79E11FF", x => new { x.USERPHONENUMBER, x.PRODUCTID, x.COLOR });
                    table.ForeignKey(
                        name: "FK__SANPHAM_G__PRODU__10566F31",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__SANPHAM_G__USERP__0F624AF8",
                        column: x => x.USERPHONENUMBER,
                        principalTable: "USERS",
                        principalColumn: "PHONENUMBER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SAMPHAM_DONHANG",
                columns: table => new
                {
                    ORDERID = table.Column<int>(type: "int", nullable: false),
                    PRODUCTID = table.Column<int>(type: "int", nullable: false),
                    COLOR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COST = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SANPHAM_DONHANG_KEY", x => new { x.ORDERID, x.PRODUCTID, x.COLOR });
                    table.ForeignKey(
                        name: "FK__SAMPHAM_D__ORDER__52593CB8",
                        column: x => x.ORDERID,
                        principalTable: "DONHANG",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__SAMPHAM_D__PRODU__534D60F1",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SANBONG_DONHANG",
                columns: table => new
                {
                    ORDERID = table.Column<int>(type: "int", nullable: false),
                    FIELDID = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    KIP = table.Column<int>(type: "int", nullable: false),
                    TIMES = table.Column<DateTime>(type: "datetime", nullable: false),
                    COST = table.Column<int>(type: "int", nullable: false),
                    NOTE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANBONG_DONHANG", x => new { x.ORDERID, x.FIELDID });
                    table.ForeignKey(
                        name: "SANBONG_DONHANG_DONHANG",
                        column: x => x.ORDERID,
                        principalTable: "DONHANG",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SANBONG_DONHANG_SANBONG",
                        column: x => x.FIELDID,
                        principalTable: "SANBONG",
                        principalColumn: "FIELDID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "THONGBAO",
                columns: table => new
                {
                    NOTIFID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDERID = table.Column<int>(type: "int", nullable: false),
                    MESSAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIME = table.Column<DateTime>(type: "datetime", nullable: true),
                    TYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__THONGBAO__B7BDB2BDABF12599", x => x.NOTIFID);
                    table.ForeignKey(
                        name: "FK__THONGBAO__ORDERI__1332DBDC",
                        column: x => x.ORDERID,
                        principalTable: "DONHANG",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_PHONENUMBER",
                table: "DONHANG",
                column: "PHONENUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_USER_USERPHONENUMBER",
                table: "PRODUCT_USER",
                column: "USERPHONENUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_SAMPHAM_DONHANG_PRODUCTID",
                table: "SAMPHAM_DONHANG",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_SANBONG_DONHANG_FIELDID",
                table: "SANBONG_DONHANG",
                column: "FIELDID");

            migrationBuilder.CreateIndex(
                name: "IX_sanbong_user_USERPHONENUMBER",
                table: "sanbong_user",
                column: "USERPHONENUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_GIOHANG_PRODUCTID",
                table: "SANPHAM_GIOHANG",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_THONGBAO_ORDERID",
                table: "THONGBAO",
                column: "ORDERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT");

            migrationBuilder.DropTable(
                name: "AccountUser");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "BANNER");

            migrationBuilder.DropTable(
                name: "InfoProduct");

            migrationBuilder.DropTable(
                name: "KHOHANG");

            migrationBuilder.DropTable(
                name: "PRODUCT_USER");

            migrationBuilder.DropTable(
                name: "SAMPHAM_DONHANG");

            migrationBuilder.DropTable(
                name: "SANBONG_DONHANG");

            migrationBuilder.DropTable(
                name: "sanbong_user");

            migrationBuilder.DropTable(
                name: "SANPHAM_GIOHANG");

            migrationBuilder.DropTable(
                name: "THONGBAO");

            migrationBuilder.DropTable(
                name: "SANBONG");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "DONHANG");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
