using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProgTerms.AppData;

public partial class ProgTermsContext : DbContext
{
    public ProgTermsContext()
    {
    }

    public ProgTermsContext(DbContextOptions<ProgTermsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Term> Terms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=ProgTerms.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Terms_Id").IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
