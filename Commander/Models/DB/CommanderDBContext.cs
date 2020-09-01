using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Commander.Models.DB
{
    public partial class CommanderDBContext : DbContext
    {
        public CommanderDBContext()
        {
        }

        public CommanderDBContext(DbContextOptions<CommanderDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Role> Role { get; set; }



        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblVendor> TblVendor { get; set; }



        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }



//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=LAPTOP-K1N85LM7;Database=CommanderDB;Trusted_Connection=True;user id=TestUser;password=pa55word;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);

            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.Property(e => e.HowTo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });





            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("tbl_department");

                entity.Property(e => e.GroupName).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tbl_product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Color).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductNumber).IsRequired();
            });

            modelBuilder.Entity<TblVendor>(entity =>
            {
                entity.HasKey(e => e.VendorId);

                entity.ToTable("tbl_vendor");

                entity.Property(e => e.Name).IsRequired();
            });

            // Register store procedure custom object
            //modelBuilder.Query<SpGetProductByPriceGreaterThan1000>();
            //modelBuilder.Query<SpGetProductByID>();
            modelBuilder.Entity<SpGetProductByPriceGreaterThan1000>().HasNoKey();
            modelBuilder.Entity<SpGetProductByID>().HasNoKey();



            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__RoleId__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__UserId__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        //#region Get products whose price is greater than equal to 100 store procedure method.
        ///// <summary
        ///// Get Products whose price is greater than equal to 1000 store procedure method.
        ///// </summary>
        ///// <returns>Returns - List of products whose price is greater than equal to 1000
        ///// </returns>
        //public async Task<List<SpGetProductByPriceGreaterThan1000>> GetProductByPriceGreaterThan1000Async()
        //{
        //    // Initialization.  
        //    List<SpGetProductByPriceGreaterThan1000> lst = new List<SpGetProductByPriceGreaterThan1000>();

        //    try
        //    {
        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[GetProductByPriceGreaterThan1000] ";

        //        lst = await this.Query<SpGetProductByPriceGreaterThan1000>().FromSqlRaw(sqlQuery).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}

        //#endregion

        //#region Get product by ID store procedure method.  

        ///// <summary>  
        ///// Get product by ID store procedure method.  
        ///// </summary>  
        ///// <param name="productId">Product ID value parameter</param>  
        ///// <returns>Returns - List of product by ID</returns>  
        //public async Task<List<SpGetProductByID>> GetProductByIDAsync(int productId)
        //{
        //    // Initialization.  
        //    List<SpGetProductByID> lst = new List<SpGetProductByID>();

        //    try
        //    {
        //        // Settings.  
        //        SqlParameter usernameParam = new SqlParameter("@product_ID", productId.ToString() ?? (object)DBNull.Value);

        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[GetProductByID] " +
        //                            "@product_ID";

        //        lst = await this.Query<SpGetProductByID>().FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}

        //#endregion
    }
}
