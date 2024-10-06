using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblGiftcard> TblGiftcards { get; set; }

    public virtual DbSet<TblPurchaseInvoice> TblPurchaseInvoices { get; set; }

    public virtual DbSet<TblPurchaseInvoiceDetail> TblPurchaseInvoiceDetails { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblGiftcard>(entity =>
        {
            entity.HasKey(e => e.GiftCardId).HasName("PRIMARY");

            entity.ToTable("tbl_giftcard");

            entity.HasIndex(e => e.GiftCardNo, "GiftCardNo_UNIQUE").IsUnique();

            entity.Property(e => e.GiftCardId).HasMaxLength(60);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.ExpiryDate).HasMaxLength(45);
            entity.Property(e => e.GiftCardNo).HasMaxLength(15);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Qrcode).HasColumnName("QRCode");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblPurchaseInvoice>(entity =>
        {
            entity.HasKey(e => e.PurchaseInvoiceId).HasName("PRIMARY");

            entity.ToTable("tbl_purchase_invoice");

            entity.HasIndex(e => e.UserId, "FK_User_idx");

            entity.HasIndex(e => e.InvoiceNo, "InvoiceNo_UNIQUE").IsUnique();

            entity.Property(e => e.PurchaseInvoiceId).HasMaxLength(60);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasMaxLength(60);
            entity.Property(e => e.PaymentMethod).HasMaxLength(60);
            entity.Property(e => e.UserId).HasMaxLength(60);

            entity.HasOne(d => d.User).WithMany(p => p.TblPurchaseInvoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        modelBuilder.Entity<TblPurchaseInvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_purchase_invoice_detail");

            entity.HasIndex(e => e.GiftCardId, "FK_GiftCard_idx");

            entity.Property(e => e.Id).HasMaxLength(60);
            entity.Property(e => e.GiftCardId).HasMaxLength(60);
            entity.Property(e => e.InvoiceNo).HasMaxLength(60);
            entity.Property(e => e.RecipientName).HasMaxLength(45);
            entity.Property(e => e.RecipientPhoneNumber).HasMaxLength(15);
            entity.Property(e => e.TypeOfBuying).HasMaxLength(45);

            entity.HasOne(d => d.GiftCard).WithMany(p => p.TblPurchaseInvoiceDetails)
                .HasForeignKey(d => d.GiftCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiftCard_Purchase_Invoice_Detail");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("tbl_user");

            entity.HasIndex(e => e.PhoneNumber, "PhoneNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "UserId_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasMaxLength(60);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserRole).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
