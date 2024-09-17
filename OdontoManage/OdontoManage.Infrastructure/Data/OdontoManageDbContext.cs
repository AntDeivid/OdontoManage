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
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Scheduling> Schedules { get; set; }

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
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Scheduling>()
            .HasOne(s => s.Patient)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Scheduling>()
            .HasOne(s => s.Dentist)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Patient>()
            .Property(p => p.Document)
            .IsRequired(false);

        modelBuilder.Entity<Patient>()
            .Property(p => p.Cpf)
            .IsRequired(false);

        modelBuilder.Entity<Patient>()
            .Property(p => p.Rg)
            .IsRequired(false);

    }
}