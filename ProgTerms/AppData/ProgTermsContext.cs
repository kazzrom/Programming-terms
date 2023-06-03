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
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ProgTerms;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Term>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
