using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace minimarket_project_backend.Models;

public partial class DbMinimarketContext : DbContext
{
    public DbMinimarketContext()
    {
    }

    public DbMinimarketContext(DbContextOptions<DbMinimarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductFlavor> ProductFlavors { get; set; }

    public virtual DbSet<ProductPresentation> ProductPresentations { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC07E6D3CECB");

            entity.ToTable("Brand");

            entity.HasIndex(e => e.Name, "IDX_Brand_Name");

            entity.Property(e => e.BrandImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07011B8B11");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "IDX_Category_Name");

            entity.HasIndex(e => e.ParentCategoryId, "IDX_Category_Parent");

            entity.Property(e => e.CategoryImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.CategoryLevel).HasDefaultValue(1);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParentCategoryId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Category_Parent");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__3214EC0717E4FEEF");

            entity.ToTable("District");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_District_Province");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC07BAF3F8C7");

            entity.ToTable("Menu");

            entity.HasIndex(e => e.ParentMenuId, "IDX_Menu_Parent");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParentMenuId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK_Menu_Parent");
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentH__3214EC07471A209E");

            entity.ToTable("PaymentHistory");

            entity.HasIndex(e => e.SaleId, "IDX_PaymentHistory_Sale");

            entity.HasIndex(e => new { e.SaleId, e.PaymentMethodId }, "UQ_PaymentHistory_Sale_Method").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentHistories)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentHistory_Method");

            entity.HasOne(d => d.Sale).WithMany(p => p.PaymentHistories)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK_PaymentHistory_Sale");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC0706903FC1");

            entity.ToTable("PaymentMethod");

            entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F64B40FF43").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethodImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC076593C232");

            entity.ToTable("Product");

            entity.HasIndex(e => e.BrandId, "IDX_Product_Brand");

            entity.HasIndex(e => e.CategoryId, "IDX_Product_Category");

            entity.HasIndex(e => e.Name, "IDX_Product_Name");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProductImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_CreatedBy");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.ProductLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK_Product_LastUpdatedBy");
        });

        modelBuilder.Entity<ProductFlavor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductF__3214EC070246647C");

            entity.ToTable("ProductFlavor");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<ProductPresentation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductP__3214EC07C374CFFF");

            entity.ToTable("ProductPresentation");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductV__3214EC07CE0F901D");

            entity.ToTable("ProductVariant");

            entity.HasIndex(e => e.BarCode, "IDX_ProductVariant_BarCode");

            entity.HasIndex(e => e.ProductId, "IDX_ProductVariant_Product");

            entity.HasIndex(e => e.Sku, "IDX_ProductVariant_Sku");

            entity.HasIndex(e => e.BarCode, "UQ__ProductV__8A2ACA9B16D153FA").IsUnique();

            entity.HasIndex(e => e.Sku, "UQ__ProductV__CA1FD3C5C459CFF2").IsUnique();

            entity.Property(e => e.BarCode)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductVariantImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductVariantCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ProductVariant_CreatedBy");

            entity.HasOne(d => d.Flavor).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.FlavorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ProductVariant_Flavor");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.ProductVariantLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK_ProductVariant_LastUpdatedBy");

            entity.HasOne(d => d.Presentation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.PresentationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ProductVariant_Presentation");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVariant_Product");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Province__3214EC0785FB14A4");

            entity.ToTable("Province");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Region).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Province_Region");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC0768AA1625");

            entity.ToTable("Region");

            entity.HasIndex(e => e.RegionCode, "UQ__Region__82432A4757108E72").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07012146F8");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UQ__Role__737584F6E5FA28FA").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role_Men__3214EC07866EEA93");

            entity.ToTable("Role_Menu");

            entity.HasIndex(e => e.MenuId, "IDX_RoleMenu_Menu");

            entity.HasIndex(e => e.RoleId, "IDX_RoleMenu_Role");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ_Role_Menu").IsUnique();

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_RoleMenu_Menu");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleMenu_Role");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role_Per__3214EC0720043730");

            entity.ToTable("Role_Permissions");

            entity.Property(e => e.CanRead).HasDefaultValue(true);
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Role_Perm__IdRol__40F9A68C");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sale__3214EC07D0810CD6");

            entity.ToTable("Sale");

            entity.HasIndex(e => e.SaleDate, "IDX_Sale_Date");

            entity.HasIndex(e => e.SystemUserId, "IDX_Sale_User");

            entity.HasIndex(e => new { e.SystemUserId, e.SaleDate }, "IDX_Sale_User_Date");

            entity.HasIndex(e => e.TransactionId, "UQ_Sale_TransactionId").IsUnique();

            entity.HasIndex(e => e.TransactionId, "UQ__Sale__55433A6AC468445F").IsUnique();

            entity.HasIndex(e => e.InvoiceNumber, "UQ__Sale__D776E981D1935318").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SaleDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SaleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Sale_CreatedBy");

            entity.HasOne(d => d.District).WithMany(p => p.Sales)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_District");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SaleLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK_Sale_LastUpdatedBy");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.SaleSystemUsers)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_SystemUser");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SaleDeta__3214EC0748666336");

            entity.ToTable("SaleDetail");

            entity.HasIndex(e => e.ProductVariantId, "IDX_SaleDetail_ProductVariant");

            entity.HasIndex(e => e.SaleId, "IDX_SaleDetail_Sale");

            entity.HasIndex(e => new { e.SaleId, e.ProductVariantId }, "IDX_SaleDetail_Sale_ProductVariant");

            entity.HasIndex(e => new { e.SaleId, e.ProductVariantId }, "UQ_SaleDetail").IsUnique();

            entity.Property(e => e.HistoricalUnitPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([Quantity]*[HistoricalUnitPrice])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetail_ProductVariant");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetail_Sale");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC0756205C82");

            entity.ToTable("SystemUser");

            entity.HasIndex(e => e.Email, "IDX_SystemUser_Email");

            entity.HasIndex(e => e.UserTypeId, "IDX_SystemUser_UserType");

            entity.HasIndex(e => e.DocumentNumber, "UQ__SystemUs__68993918033D7576").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__SystemUs__A9D10534CC139236").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.UserType).WithMany(p => p.SystemUsers)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SystemUser_UserType");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC070D33F1DF");

            entity.ToTable("UserRole");

            entity.HasIndex(e => new { e.SystemUserId, e.RoleId }, "UQ_SystemUser_Role").IsUnique();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__RoleId__6A30C649");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.SystemUserId)
                .HasConstraintName("FK__UserRole__System__693CA210");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserType__3214EC0737A61136");

            entity.ToTable("UserType");

            entity.HasIndex(e => e.Name, "UQ__UserType__737584F612BE7D76").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
