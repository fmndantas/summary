using Microsoft.EntityFrameworkCore;

namespace summary.Models
{
    public class SummaryContext : DbContext
    {
        public DbSet<Summary> Summaries { get; set; }

        public SummaryContext(DbContextOptions<SummaryContext> options) : base(options)
        {
        }
    }
}