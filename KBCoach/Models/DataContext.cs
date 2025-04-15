using Microsoft.EntityFrameworkCore;
using KBCoach.Models;

namespace KBCoach.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public DbSet<MiscModel.TransactionModel> Transactions { get; set; }
}

