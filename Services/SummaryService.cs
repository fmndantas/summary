using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using summary.Models;

namespace summary.Services
{
    public class SummaryService : ISummaryService
    {
        private DbContext _context;

        public SummaryService(SummaryContext context)
        {
            _context = context;
        }

        public List<Summary> FindAll()
        {
            return _context
                .Set<Summary>()
                .ToList();
        }

        public Summary FindById(int id)
        {
            return _context
                .Set<Summary>()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}