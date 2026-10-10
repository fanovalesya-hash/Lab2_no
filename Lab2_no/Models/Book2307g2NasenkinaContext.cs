using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2_no.Models;

public partial class Book2307g2NasenkinaContext : DbContext
{
    public Book2307g2NasenkinaContext()
    {
    }

    public Book2307g2NasenkinaContext(DbContextOptions<Book2307g2NasenkinaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Book_2307g2_Nasenkina;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC14233EE75D");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07AA4E344E");

            entity.Property(e => e.FkGenreId).HasColumnName("fk_GenreID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.FkGenre).WithMany(p => p.Books)
                .HasForeignKey(d => d.FkGenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__fk_GenreI__71D1E811");

            entity.HasMany(d => d.FkAuthors).WithMany(p => p.FkBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsBook",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("FkAuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_B__fk_Au__75A278F5"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("FkBookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Authors_B__fk_Bo__74AE54BC"),
                    j =>
                    {
                        j.HasKey("FkBookId", "FkAuthorId").HasName("PK__Authors___DE6B61DA58492360");
                        j.ToTable("Authors_Books");
                        j.IndexerProperty<int>("FkBookId").HasColumnName("fk_BookID");
                        j.IndexerProperty<int>("FkAuthorId").HasColumnName("fk_AuthorID");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genres__3214EC07CAA4ED14");

            entity.Property(e => e.Nickname).HasMaxLength(50);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Loans__3214EC07795969C5");

            entity.Property(e => e.DateLoans).HasColumnName("Date_loans");
            entity.Property(e => e.FkBookId).HasColumnName("fk_BookID");
            entity.Property(e => e.FkUserId).HasColumnName("fk_UserID");

            entity.HasOne(d => d.FkBook).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkBookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__fk_BookID__797309D9");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__fk_UserID__787EE5A0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07D002D348");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160F5BD4094").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0731E38E88");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B3D9ECDD9").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__6EF57B66");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
