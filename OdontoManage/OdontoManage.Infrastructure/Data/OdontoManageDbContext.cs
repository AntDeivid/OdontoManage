using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using OdontoManage.Core.Models;

namespace OdontoManage.Infrastructure.Data;

public class OdontoManageDbContext : DbContext
{
    public OdontoManageDbContext(DbContextOptions<OdontoManageDbContext> options) : base(options) { }
    
    public DbSet<User>? Users { get; set; }
    public DbSet<Patient>? Patients { get; set; }
    public DbSet<Dentist>? Dentists { get; set; }
    public DbSet<Treatment>? Treatments { get; set; }
    public DbSet<ClinicalTreatment>? ClinicalTreatments { get; set; }
    public DbSet<Revenue>? Revenues { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Item>? Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração de Revenue com seus tratamentos
        modelBuilder.Entity<Revenue>()
            .HasMany(r => r.Treatments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de Treatment
        modelBuilder.Entity<Treatment>()
            .HasOne(t => t.Patient)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Treatment>()
            .HasOne(t => t.ClinicalTreatment)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Treatment>()
            .HasOne(t => t.Dentist)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Address)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);
            

        // Configurações adicionais
        modelBuilder.Entity<Patient>()
            .Property(p => p.BirthDay)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));
            
        modelBuilder.Entity<Treatment>()
            .Property(t => t.InstallmentDueDate)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

        modelBuilder.Entity<Expense>()
            .Property(e => e.InstallmentDueDate)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));
            
        modelBuilder.Entity<Revenue>()
            .Property(r => r.InstallmentDueDate)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));
    }
}