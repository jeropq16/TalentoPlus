using _2_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3_Infrastructure.Data;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions <AppDbContext> options) : base(options) { }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(e => e.Documento)
                .IsUnique();

            entity.Property(e => e.Nombres)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Cargo)
                .IsRequired()
                .HasMaxLength(100);

            // Guardar enums como string
            entity.Property(e => e.Estado)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.NivelEducativo)
                .HasConversion<string>()
                .HasMaxLength(30);

            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
        });

        // Department
        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(d => d.Nombre)
                .IsUnique();
        });
    }
}