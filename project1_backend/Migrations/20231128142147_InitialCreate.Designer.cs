﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project1_backend.Models;

#nullable disable

namespace project1_backend.Migrations
{
    [DbContext(typeof(ProjectBongDaContext))]
    [Migration("20231128142147_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("project1_backend.Models.Account", b =>
                {
                    b.Property<string>("Phonenumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("PHONENUMBER")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("PASSWORD");

                    b.HasKey("Phonenumber")
                        .HasName("PK__ACCOUNT__8F2B07B04C9DA7A6");

                    b.ToTable("ACCOUNT", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Admin", b =>
                {
                    b.Property<string>("Account")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Account");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Banner", b =>
                {
                    b.Property<int>("Bannerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BANNERID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Bannerid"));

                    b.Property<string>("Linkimg")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LINKIMG");

                    b.Property<string>("Title")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("TITLE");

                    b.HasKey("Bannerid")
                        .HasName("PK__BANNER__0F8F784DAF66C86F");

                    b.ToTable("BANNER", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Custom.AccountUser", b =>
                {
                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhoneNumber");

                    b.ToTable("AccountUser");
                });

            modelBuilder.Entity("project1_backend.Models.Custom.InfoProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Linkimg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("InfoProduct");
                });

            modelBuilder.Entity("project1_backend.Models.Donhang", b =>
                {
                    b.Property<int>("Orderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ORDERID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Orderid"));

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("PHONENUMBER")
                        .IsFixedLength();

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("STATUS");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime2")
                        .HasColumnName("TIME");

                    b.Property<int>("Totalcost")
                        .HasColumnType("int")
                        .HasColumnName("TOTALCOST");

                    b.HasKey("Orderid")
                        .HasName("PK__DONHANG__491E419249F835B4");

                    b.HasIndex("Phonenumber");

                    b.ToTable("DONHANG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Khohang", b =>
                {
                    b.Property<int>("Productid")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCTID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.HasKey("Productid")
                        .HasName("PK__KHOHANG__34980AA23774C1AD");

                    b.ToTable("KHOHANG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Product", b =>
                {
                    b.Property<int>("Productid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PRODUCTID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Productid"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("COLOR");

                    b.Property<string>("Detail")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("DETAIL");

                    b.Property<string>("Linkimg")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("LINKIMG");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("PRICE");

                    b.Property<string>("Productname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("PRODUCTNAME");

                    b.Property<int?>("Rate")
                        .HasColumnType("int")
                        .HasColumnName("RATE");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("TYPE");

                    b.HasKey("Productid")
                        .HasName("PK__PRODUCT__34980AA2B87F5836");

                    b.ToTable("PRODUCT", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.ProductUser", b =>
                {
                    b.Property<int>("Productid")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCTID");

                    b.Property<string>("Userphonenumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("USERPHONENUMBER")
                        .IsFixedLength();

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COMMENT");

                    b.Property<int?>("Rate")
                        .HasColumnType("int")
                        .HasColumnName("RATE");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime")
                        .HasColumnName("TIME");

                    b.HasKey("Productid", "Userphonenumber")
                        .HasName("PK__PRODUCT___64227F3B363158BE");

                    b.HasIndex("Userphonenumber");

                    b.ToTable("PRODUCT_USER", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.SamphamDonhang", b =>
                {
                    b.Property<int>("Orderid")
                        .HasColumnType("int")
                        .HasColumnName("ORDERID");

                    b.Property<int>("Productid")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCTID");

                    b.Property<int>("Cost")
                        .HasColumnType("int")
                        .HasColumnName("COST");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.HasKey("Orderid", "Productid")
                        .HasName("SANPHAM_DONHANG_KEY");

                    b.HasIndex("Productid");

                    b.ToTable("SAMPHAM_DONHANG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Sanbong", b =>
                {
                    b.Property<string>("Fieldid")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("char(5)")
                        .HasColumnName("FIELDID")
                        .IsFixedLength();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ADDRESS");

                    b.Property<int>("Cost")
                        .HasColumnType("int")
                        .HasColumnName("COST");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DECRIPTION");

                    b.Property<string>("Linkimg")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("LINKIMG");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("NAME");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("PRICE");

                    b.Property<int>("Rate")
                        .HasColumnType("int")
                        .HasColumnName("RATE");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("TYPE");

                    b.HasKey("Fieldid")
                        .HasName("PK__SANBONG__707268028301F47D");

                    b.ToTable("SANBONG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.SanbongDonhang", b =>
                {
                    b.Property<int>("Orderid")
                        .HasColumnType("int")
                        .HasColumnName("ORDERID");

                    b.Property<string>("Fieldid")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("char(5)")
                        .HasColumnName("FIELDID")
                        .IsFixedLength();

                    b.Property<int>("Cost")
                        .HasColumnType("int")
                        .HasColumnName("COST");

                    b.Property<int>("Kip")
                        .HasColumnType("int")
                        .HasColumnName("KIP");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NOTE");

                    b.Property<DateTime>("Times")
                        .HasColumnType("datetime")
                        .HasColumnName("TIMES");

                    b.HasKey("Orderid", "Fieldid");

                    b.HasIndex("Fieldid");

                    b.ToTable("SANBONG_DONHANG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.SanbongUser", b =>
                {
                    b.Property<string>("Fieldid")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("char(5)")
                        .HasColumnName("FIELDID")
                        .IsFixedLength();

                    b.Property<string>("Userphonenumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("USERPHONENUMBER")
                        .IsFixedLength();

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COMMENT");

                    b.Property<int?>("Rate")
                        .HasColumnType("int")
                        .HasColumnName("RATE");

                    b.HasKey("Fieldid", "Userphonenumber")
                        .HasName("PK_SANBONG_USER");

                    b.HasIndex("Userphonenumber");

                    b.ToTable("sanbong_user", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.SanphamGiohang", b =>
                {
                    b.Property<string>("Userphonenumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("USERPHONENUMBER")
                        .IsFixedLength();

                    b.Property<int>("Productid")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCTID");

                    b.Property<int?>("Price")
                        .HasColumnType("int")
                        .HasColumnName("PRICE");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.HasKey("Userphonenumber", "Productid")
                        .HasName("PK__SANPHAM___38EED93EB79E11FF");

                    b.HasIndex("Productid");

                    b.ToTable("SANPHAM_GIOHANG", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Thongbao", b =>
                {
                    b.Property<int>("Notifid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NOTIFID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Notifid"));

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MESSAGE");

                    b.Property<int>("Orderid")
                        .HasColumnType("int")
                        .HasColumnName("ORDERID");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime")
                        .HasColumnName("TIME");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("TYPE");

                    b.HasKey("Notifid")
                        .HasName("PK__THONGBAO__B7BDB2BDABF12599");

                    b.HasIndex("Orderid");

                    b.ToTable("THONGBAO", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.User", b =>
                {
                    b.Property<string>("Phonenumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("PHONENUMBER")
                        .IsFixedLength();

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("ADDRESS");

                    b.Property<string>("Avt")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("AVT");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("BIRTHDATE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NAME");

                    b.HasKey("Phonenumber")
                        .HasName("PK__USERS__8F2B07B0110AA2A0");

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("project1_backend.Models.Account", b =>
                {
                    b.HasOne("project1_backend.Models.User", "PhonenumberNavigation")
                        .WithOne("Account")
                        .HasForeignKey("project1_backend.Models.Account", "Phonenumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ACCOUNT_USERS");

                    b.Navigation("PhonenumberNavigation");
                });

            modelBuilder.Entity("project1_backend.Models.Donhang", b =>
                {
                    b.HasOne("project1_backend.Models.User", "PhonenumberNavigation")
                        .WithMany("Donhangs")
                        .HasForeignKey("Phonenumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("DONHANGG_USER");

                    b.Navigation("PhonenumberNavigation");
                });

            modelBuilder.Entity("project1_backend.Models.Khohang", b =>
                {
                    b.HasOne("project1_backend.Models.Product", "Product")
                        .WithOne("Khohang")
                        .HasForeignKey("project1_backend.Models.Khohang", "Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__KHOHANG__PRODUCT__3B75D760");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("project1_backend.Models.ProductUser", b =>
                {
                    b.HasOne("project1_backend.Models.Product", "Product")
                        .WithMany("ProductUsers")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_3");

                    b.HasOne("project1_backend.Models.User", "UserphonenumberNavigation")
                        .WithMany("ProductUsers")
                        .HasForeignKey("Userphonenumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_4");

                    b.Navigation("Product");

                    b.Navigation("UserphonenumberNavigation");
                });

            modelBuilder.Entity("project1_backend.Models.SamphamDonhang", b =>
                {
                    b.HasOne("project1_backend.Models.Donhang", "Order")
                        .WithMany("SamphamDonhangs")
                        .HasForeignKey("Orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__SAMPHAM_D__ORDER__52593CB8");

                    b.HasOne("project1_backend.Models.Product", "Product")
                        .WithMany("SamphamDonhangs")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__SAMPHAM_D__PRODU__534D60F1");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("project1_backend.Models.SanbongDonhang", b =>
                {
                    b.HasOne("project1_backend.Models.Sanbong", "Field")
                        .WithMany("SanbongDonhangs")
                        .HasForeignKey("Fieldid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("SANBONG_DONHANG_SANBONG");

                    b.HasOne("project1_backend.Models.Donhang", "Order")
                        .WithMany("SanbongDonhangs")
                        .HasForeignKey("Orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("SANBONG_DONHANG_DONHANG");

                    b.Navigation("Field");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("project1_backend.Models.SanbongUser", b =>
                {
                    b.HasOne("project1_backend.Models.Sanbong", "Field")
                        .WithMany("SanbongUsers")
                        .HasForeignKey("Fieldid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_1");

                    b.HasOne("project1_backend.Models.User", "UserphonenumberNavigation")
                        .WithMany("SanbongUsers")
                        .HasForeignKey("Userphonenumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_2");

                    b.Navigation("Field");

                    b.Navigation("UserphonenumberNavigation");
                });

            modelBuilder.Entity("project1_backend.Models.SanphamGiohang", b =>
                {
                    b.HasOne("project1_backend.Models.Product", "Product")
                        .WithMany("SanphamGiohangs")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__SANPHAM_G__PRODU__10566F31");

                    b.HasOne("project1_backend.Models.User", "UserphonenumberNavigation")
                        .WithMany("SanphamGiohangs")
                        .HasForeignKey("Userphonenumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__SANPHAM_G__USERP__0F624AF8");

                    b.Navigation("Product");

                    b.Navigation("UserphonenumberNavigation");
                });

            modelBuilder.Entity("project1_backend.Models.Thongbao", b =>
                {
                    b.HasOne("project1_backend.Models.Donhang", "Order")
                        .WithMany("Thongbaos")
                        .HasForeignKey("Orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__THONGBAO__ORDERI__1332DBDC");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("project1_backend.Models.Donhang", b =>
                {
                    b.Navigation("SamphamDonhangs");

                    b.Navigation("SanbongDonhangs");

                    b.Navigation("Thongbaos");
                });

            modelBuilder.Entity("project1_backend.Models.Product", b =>
                {
                    b.Navigation("Khohang");

                    b.Navigation("ProductUsers");

                    b.Navigation("SamphamDonhangs");

                    b.Navigation("SanphamGiohangs");
                });

            modelBuilder.Entity("project1_backend.Models.Sanbong", b =>
                {
                    b.Navigation("SanbongDonhangs");

                    b.Navigation("SanbongUsers");
                });

            modelBuilder.Entity("project1_backend.Models.User", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Donhangs");

                    b.Navigation("ProductUsers");

                    b.Navigation("SanbongUsers");

                    b.Navigation("SanphamGiohangs");
                });
#pragma warning restore 612, 618
        }
    }
}
