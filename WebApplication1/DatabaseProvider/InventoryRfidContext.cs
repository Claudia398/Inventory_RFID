using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DatabaseProvider;

public partial class InventoryRfidContext : DbContext
{
    public InventoryRfidContext()
    {
    }

    public InventoryRfidContext(DbContextOptions<InventoryRfidContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostCenter> CostCenters { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<NrInventory> NrInventories { get; set; }

    public virtual DbSet<NrSubInventory> NrSubInventories { get; set; }

    public virtual DbSet<Placement> Placements { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ScanRfid> ScanRfids { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Inventory_RFID;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostCenter>(entity =>
        {
            entity.ToTable("cost_center");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Center)
                .HasMaxLength(100)
                .HasColumnName("center");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            entity.Property(e => e.NrInventoryId).HasColumnName("nr_inventory_id");
            entity.Property(e => e.PlacementId).HasColumnName("placement_id");
            entity.Property(e => e.Uid)
                .HasMaxLength(50)
                .HasColumnName("UID");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.InventoryCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_users1");

            entity.HasOne(d => d.NrInventory).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.NrInventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_nr_inventar");

            entity.HasOne(d => d.Placement).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.PlacementId)
                .HasConstraintName("FK_inventory_placement");

            entity.HasOne(d => d.User).WithMany(p => p.InventoryUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_inventory_users");
        });

        modelBuilder.Entity<NrInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nr_inventar_1");

            entity.ToTable("nr_inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CostCenterId).HasColumnName("cost_center_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Rfid)
                .HasMaxLength(50)
                .HasColumnName("RFID");
            entity.Property(e => e.Serial)
                .HasMaxLength(150)
                .HasColumnName("serial");

            entity.HasOne(d => d.CostCenter).WithMany(p => p.NrInventories)
                .HasForeignKey(d => d.CostCenterId)
                .HasConstraintName("FK_nr_inventar_cost_center");
        });

        modelBuilder.Entity<NrSubInventory>(entity =>
        {
            entity.ToTable("nr_sub_inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.NrInventoryId).HasColumnName("nr_inventory_id");

            entity.HasOne(d => d.NrInventory).WithMany(p => p.NrSubInventories)
                .HasForeignKey(d => d.NrInventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nr_sub_inventory_nr_inventory");
        });

        modelBuilder.Entity<Placement>(entity =>
        {
            entity.ToTable("placement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ScanRfid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Scan_RFID");

            entity.ToTable("scan_RFID");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.PlacementId).HasColumnName("placement_id");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ScanRfids)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Scan_RFID_inventory");

            entity.HasOne(d => d.Placement).WithMany(p => p.ScanRfids)
                .HasForeignKey(d => d.PlacementId)
                .HasConstraintName("FK_Scan_RFID_placement");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(150)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
