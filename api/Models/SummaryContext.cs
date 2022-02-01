using Microsoft.EntityFrameworkCore;
using summary.Models;

namespace api.Models
{
    public class SummaryContext : DbContext
    {
        public DbSet<Summary> Summaries { get; set; }

        public SummaryContext(DbContextOptions<SummaryContext> options) : base(options)
        {
        }
    }
}