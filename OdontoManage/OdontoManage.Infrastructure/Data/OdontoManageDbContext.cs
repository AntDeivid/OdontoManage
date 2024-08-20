using Microsoft.EntityFrameworkCore;
using OdontoManage.Core.Models;

namespace OdontoManage.Infrastructure.Data;

public class OdontoManageDbContext : DbContext
{
    public OdontoManageDbContext(DbContextOptions<OdontoManageDbContext> options) : base(options) { }
    
    public DbSet<User>? Users { get; set; }
}