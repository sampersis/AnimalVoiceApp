using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Database;

public partial class AnimalsdbContext : DbContext
{
    public AnimalsdbContext()
    {
    }

    public AnimalsdbContext(DbContextOptions<AnimalsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__animals__3214EC078C0D0CF9");

            entity.ToTable("animals");

            entity.HasIndex(e => e.AnimalName, "UQ__animals__4B22B22B8A91E334").IsUnique();

            entity.Property(e => e.AnimalName)
                .HasMaxLength(50)
                .HasColumnName("animalName");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.VideoUrl).HasColumnName("videoUrl");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
