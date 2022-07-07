using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Market_store.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Category1> Category1s { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Mainpage> Mainpages { get; set; }
        public virtual DbSet<Order1> Order1s { get; set; }
        public virtual DbSet<Orderproduct> Orderproducts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product1> Product1s { get; set; }
        public virtual DbSet<Productshop> Productshops { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shop1> Shop1s { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Useraccount> Useraccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User13;PASSWORD=Ahmad118513;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER13")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutid)
                    .HasName("SYS_C00237201");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTID");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Text1)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Text3)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT3");

                entity.Property(e => e.Text4)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT4");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00237189");

                entity.ToTable("BANK");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");

                entity.Property(e => e.Expiration)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRATION");

                entity.Property(e => e.Ownername)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OWNERNAME");
            });

            modelBuilder.Entity<Category1>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("SYS_C00237168");

                entity.ToTable("CATEGORY1");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Contid)
                    .HasName("SYS_C00237197");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Contid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Mainpage>(entity =>
            {
                entity.HasKey(e => e.Homeid)
                    .HasName("SYS_C00237199");

                entity.ToTable("MAINPAGE");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Companyemail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYEMAIL");

                entity.Property(e => e.Companylogo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYLOGO");

                entity.Property(e => e.Companyphone)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYPHONE");

                entity.Property(e => e.Image1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE1");

                entity.Property(e => e.Image2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE2");

                entity.Property(e => e.Text1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");
            });

            modelBuilder.Entity<Order1>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("SYS_C00237182");

                entity.ToTable("ORDER1");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDERDATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order1s)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKUSER");
            });

            modelBuilder.Entity<Orderproduct>(entity =>
            {
                entity.ToTable("ORDERPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Numberofpieces)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFPIECES");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SHOPID");

                entity.Property(e => e.Status)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Totalamount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTALAMOUNT");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PRODUCTFK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PRODUCT");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00237191");

                entity.ToTable("PAYMENT");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Paydate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYDATE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("USERSFK");
            });

            modelBuilder.Entity<Product1>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("SYS_C00237173");

                entity.ToTable("PRODUCT1");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Productsize)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTSIZE");

                entity.Property(e => e.Productvalue)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRODUCTVALUE");
            });

            modelBuilder.Entity<Productshop>(entity =>
            {
                entity.ToTable("PRODUCTSHOP");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SHOPID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productshops)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKPRODUCT");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Productshops)
                    .HasForeignKey(d => d.Shopid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("F_CUSTOMER");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Shop1>(entity =>
            {
                entity.HasKey(e => e.Shopid)
                    .HasName("SYS_C00237170");

                entity.ToTable("SHOP1");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SHOPID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Monthlyrent)
                    .HasColumnType("FLOAT")
                    .HasColumnName("MONTHLYRENT");

                entity.Property(e => e.Shopname)
                    .HasMaxLength(126)
                    .IsUnicode(false)
                    .HasColumnName("SHOPNAME");

                entity.Property(e => e.Totalsales)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTALSALES");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Shop1s)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("CATEGOFK");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(e => e.Testmoninalid)
                    .HasName("SYS_C00237194");

                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Testmoninalid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTMONINALID");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Testimage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMAGE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("TESTM");
            });

            modelBuilder.Entity<Useraccount>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00237179");

                entity.ToTable("USERACCOUNT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Useraccounts)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ROLEFK");
            });

            modelBuilder.HasSequence("ID_GENERATED").IncrementsBy(2);

            modelBuilder.HasSequence("REVSEQ").IncrementsBy(-1);

            modelBuilder.HasSequence("UAE_SEQUNCE").IncrementsBy(10);

            modelBuilder.HasSequence("UAE_SEQUNCE_1").IncrementsBy(10);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
