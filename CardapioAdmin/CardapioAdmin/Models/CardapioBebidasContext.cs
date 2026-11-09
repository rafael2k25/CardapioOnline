using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CardapioAdmin.Models;

public partial class CardapioBebidasContext : DbContext
{
    public CardapioBebidasContext()
    {
    }

    public CardapioBebidasContext(DbContextOptions<CardapioBebidasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC03LAB2788\\SENAI;Database=CardapioBebidas; user id=sa;password=senai.123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07CCEA0042");

            entity.Property(e => e.Ativa).HasDefaultValue(true);
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produtos__3214EC07D55AED00");

            entity.HasIndex(e => e.CategoriaId, "IX_Produtos_CategoriaId");

            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.DataCadastro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Descricao)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Disponivel).HasDefaultValue(true);
            entity.Property(e => e.ImagemUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produtos_Categorias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
