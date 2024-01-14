using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using project1_backend.Models.Custom;

namespace project1_backend.Models;

public partial class ProjectBongDaContext : DbContext
{
    public ProjectBongDaContext()
    {
    }

    public ProjectBongDaContext(DbContextOptions<ProjectBongDaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Khohang> Khohangs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductUser> ProductUsers { get; set; }

    public virtual DbSet<SamphamDonhang> SamphamDonhangs { get; set; }

    public virtual DbSet<Sanbong> Sanbongs { get; set; }

    public virtual DbSet<SanbongDonhang> SanbongDonhangs { get; set; }

    public virtual DbSet<SanbongUser> SanbongUsers { get; set; }

    public virtual DbSet<SanphamGiohang> SanphamGiohangs { get; set; }

    public virtual DbSet<Thongbao> Thongbaos { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
  //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //
 //       => optionsBuilder.UseSqlServer("server=hoang;Data Source=.; database=ProjectBongDa1; Integrated security=true;TrustServerCertificate=True");

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

         => optionsBuilder.UseSqlServer("Server=qlbongda.mssql.somee.com;Initial Catalog=qlbongda;Persist Security Info=False;User ID=k58a01mmh_SQLLogin_1;Password=r5rhbv2ajc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Phonenumber).HasName("PK__ACCOUNT__8F2B07B04C9DA7A6");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Phonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");

            entity.HasOne(d => d.PhonenumberNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.Phonenumber)
                .HasConstraintName("FK_ACCOUNT_USERS");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Account);

            entity.ToTable("Admin");

            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Bannerid).HasName("PK__BANNER__0F8F784DAF66C86F");

            entity.ToTable("BANNER");

            entity.Property(e => e.Bannerid).HasColumnName("BANNERID");
            entity.Property(e => e.Linkimg).HasColumnName("LINKIMG");
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("PK__DONHANG__491E419249F835B4");

            entity.ToTable("DONHANG");

            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
            entity.Property(e => e.Totalcost).HasColumnName("TOTALCOST");
            entity.Property(e=>e.Time).HasColumnName("TIME");
            entity.HasOne(d => d.PhonenumberNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.Phonenumber)
                .HasConstraintName("DONHANGG_USER");
        });

        modelBuilder.Entity<Khohang>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("PK__KHOHANG__34980AA23774C1AD");

            entity.ToTable("KHOHANG");

            entity.Property(e => e.Productid)
                .ValueGeneratedNever()
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Product).WithOne(p => p.Khohang)
                .HasForeignKey<Khohang>(d => d.Productid)
                .HasConstraintName("FK__KHOHANG__PRODUCT__3B75D760");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("PK__PRODUCT__34980AA2B87F5836");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("COLOR");
            entity.Property(e => e.Detail)
                .HasMaxLength(500)
                .HasColumnName("DETAIL");
            entity.Property(e => e.Linkimg)
                .IsUnicode(false)
                .HasColumnName("LINKIMG");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Productname)
                .HasMaxLength(200)
                .HasColumnName("PRODUCTNAME");
            entity.Property(e => e.Rate).HasColumnName("RATE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<ProductUser>(entity =>
        {
            entity.HasKey(e => new { e.Productid, e.Userphonenumber,e.Time}).HasName("PK__PRODUCT___64227F3B363158BE");

            entity.ToTable("PRODUCT_USER");

            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");
            entity.Property(e => e.Userphonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("USERPHONENUMBER");
            entity.Property(e => e.Comment).HasColumnName("COMMENT");
            entity.Property(e => e.Rate).HasColumnName("RATE");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("TIME").IsRequired();
            
            entity.HasOne(d => d.Product).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("FK_3");

            entity.HasOne(d => d.UserphonenumberNavigation).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.Userphonenumber)
                .HasConstraintName("FK_4");
        });

        modelBuilder.Entity<SamphamDonhang>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Productid,e.Color }).HasName("SANPHAM_DONHANG_KEY");

            entity.ToTable("SAMPHAM_DONHANG");

            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");
            entity.Property(e=>e.Color).HasColumnName("COLOR");
            entity.Property(e => e.Cost).HasColumnName("COST");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Order).WithMany(p => p.SamphamDonhangs)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("FK__SAMPHAM_D__ORDER__52593CB8");

            entity.HasOne(d => d.Product).WithMany(p => p.SamphamDonhangs)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("FK__SAMPHAM_D__PRODU__534D60F1");
        });

        modelBuilder.Entity<Sanbong>(entity =>
        {
            entity.HasKey(e => e.Fieldid).HasName("PK__SANBONG__707268028301F47D");

            entity.ToTable("SANBONG");

            entity.Property(e => e.Fieldid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FIELDID");
            entity.Property(e => e.Address).HasColumnName("ADDRESS");
            entity.Property(e => e.Cost).HasColumnName("COST");
            entity.Property(e => e.Decription).HasColumnName("DECRIPTION");
            entity.Property(e => e.Linkimg)
                .IsUnicode(false)
                .HasColumnName("LINKIMG");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("NAME");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Rate).HasColumnName("RATE");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<SanbongDonhang>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Fieldid });

            entity.ToTable("SANBONG_DONHANG");

            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Fieldid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FIELDID");
            entity.Property(e => e.Cost).HasColumnName("COST");
            entity.Property(e => e.Kip).HasColumnName("KIP");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Times)
                .HasColumnType("datetime")
                .HasColumnName("TIMES");

            entity.HasOne(d => d.Field).WithMany(p => p.SanbongDonhangs)
                .HasForeignKey(d => d.Fieldid)
                .HasConstraintName("SANBONG_DONHANG_SANBONG");

            entity.HasOne(d => d.Order).WithMany(p => p.SanbongDonhangs)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("SANBONG_DONHANG_DONHANG");
        });

        modelBuilder.Entity<SanbongUser>(entity =>
        {
            entity.HasKey(e => new { e.Fieldid, e.Userphonenumber,e.Time }).HasName("PK_SANBONG_USER");

            entity.ToTable("sanbong_user");

            entity.Property(e => e.Fieldid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FIELDID");
            entity.Property(e => e.Userphonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("USERPHONENUMBER");
            entity.Property(e => e.Time)
                .HasColumnName("Time");
            entity.Property(e => e.Comment).HasColumnName("COMMENT");
            entity.Property(e => e.Rate).HasColumnName("RATE");

            entity.HasOne(d => d.Field).WithMany(p => p.SanbongUsers)
                .HasForeignKey(d => d.Fieldid)
                .HasConstraintName("FK_1");

            entity.HasOne(d => d.UserphonenumberNavigation).WithMany(p => p.SanbongUsers)
                .HasForeignKey(d => d.Userphonenumber)
                .HasConstraintName("FK_2");
        });

        modelBuilder.Entity<SanphamGiohang>(entity =>
        {
            entity.HasKey(e => new { e.Userphonenumber, e.Productid,e.Color }).HasName("PK__SANPHAM___38EED93EB79E11FF");

            entity.ToTable("SANPHAM_GIOHANG");

            entity.Property(e => e.Userphonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("USERPHONENUMBER");
            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Color).HasColumnName("COLOR");
            entity.HasOne(d => d.Product).WithMany(p => p.SanphamGiohangs)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("FK__SANPHAM_G__PRODU__10566F31");

            entity.HasOne(d => d.UserphonenumberNavigation).WithMany(p => p.SanphamGiohangs)
                .HasForeignKey(d => d.Userphonenumber)
                .HasConstraintName("FK__SANPHAM_G__USERP__0F624AF8");
        });

        modelBuilder.Entity<Thongbao>(entity =>
        {
            entity.HasKey(e => e.Notifid).HasName("PK__THONGBAO__B7BDB2BDABF12599");

            entity.ToTable("THONGBAO");

            entity.Property(e => e.Notifid).HasColumnName("NOTIFID");
            entity.Property(e => e.Message).HasColumnName("MESSAGE");
            entity.Property(e => e.Orderid).HasColumnName("ORDERID");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("TIME");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("TYPE");

            entity.HasOne(d => d.Order).WithMany(p => p.Thongbaos)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("FK__THONGBAO__ORDERI__1332DBDC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Phonenumber).HasName("PK__USERS__8F2B07B0110AA2A0");

            entity.ToTable("USERS");

            entity.Property(e => e.Phonenumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Gender)
            .HasColumnName("GENDER");
            entity.Property(e => e.Avt)
                .IsUnicode(false)
                .HasColumnName("AVT");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("BIRTHDATE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<project1_backend.Models.Custom.AccountUser> AccountUser { get; set; } = default!;

    public DbSet<project1_backend.Models.Custom.InfoProduct> InfoProduct { get; set; } = default!;
}
