using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Firma.Modele;

public partial class FirmaContext : DbContext
{
    public FirmaContext()
    {
    }

    public FirmaContext(DbContextOptions<FirmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brygady> BRygady { get; set; }

    public virtual DbSet<Godziny> GOdziny { get; set; }

    public virtual DbSet<Login> Loginy { get; set; }

    public virtual DbSet<Pracownicy> PRacownicy { get; set; }

    public virtual DbSet<Urlopy> URlopy { get; set; }

    public virtual DbSet<Zaliczki> ZAliczki { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source = PC\\SQLEXPRESS;Initial Catalog = Firma; Integrated Security = True; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brygady>(entity =>
        {
            entity.HasKey(e => e.BId).HasName("PK__Brygady__4E29C30D48160434");

            entity.ToTable("Brygady");

            entity.Property(e => e.BId).HasColumnName("b_id");
            entity.Property(e => e.PId).HasColumnName("p_id");

        });

        modelBuilder.Entity<Godziny>(entity =>
        {
            entity.HasKey(e => e.GId).HasName("PK__Godziny__49FB61C4F96169F0");

            entity.ToTable("Godziny");

            entity.Property(e => e.GId).HasColumnName("g_id");
            entity.Property(e => e.DataDnia).HasColumnName("data_dnia");
            entity.Property(e => e.Ilosc).HasColumnName("ilosc");
            entity.Property(e => e.PId).HasColumnName("p_id");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Login__F3DBC573C4CE808E");

            entity.ToTable("Login");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.BId).HasColumnName("b_id");
            entity.Property(e => e.Passw)
                .HasMaxLength(100)
                .HasColumnName("passw");
            entity.Property(e => e.Rola)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("rola");
        });

        modelBuilder.Entity<Pracownicy>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Pracowni__82E06B91EE349CD9");

            entity.ToTable("Pracownicy");

            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Adres)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("adres");
            entity.Property(e => e.BId).HasColumnName("b_id");
            entity.Property(e => e.DataPrzyjecia).HasColumnName("data_przyjecia");
            entity.Property(e => e.DataUrodzenia).HasColumnName("data_urodzenia");
            entity.Property(e => e.Imie)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("imie");
            entity.Property(e => e.KodPocztowy)
                .HasMaxLength(6)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("kod_pocztowy");
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("nazwisko");
            entity.Property(e => e.Stanowisko)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("stanowisko");
            entity.Property(e => e.Stawka).HasColumnName("stawka");
        });

        modelBuilder.Entity<Urlopy>(entity =>
        {
            entity.HasKey(e => e.UId).HasName("PK__Urlopy__B51D3DEACE468DE0");

            entity.ToTable("Urlopy");

            entity.Property(e => e.UId).HasColumnName("u_id");
            entity.Property(e => e.DataRozpoczecia).HasColumnName("data_rozpoczecia");
            entity.Property(e => e.DataZakonczenia).HasColumnName("data_zakonczenia");
            entity.Property(e => e.Dni)
                .HasComputedColumnSql("(datediff(day,[data_rozpoczecia],[data_zakonczenia])+(1))", false)
                .HasColumnName("dni");
            entity.Property(e => e.PId).HasColumnName("p_id");
        });

        modelBuilder.Entity<Zaliczki>(entity =>
        {
            entity.HasKey(e => e.ZId).HasName("PK__Zaliczki__977743E676EDED05");

            entity.ToTable("Zaliczki");

            entity.Property(e => e.ZId).HasColumnName("z_id");
            entity.Property(e => e.DataZaliczki).HasColumnName("data_zaliczki");
            entity.Property(e => e.Kwota).HasColumnName("kwota");
            entity.Property(e => e.PId).HasColumnName("p_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
