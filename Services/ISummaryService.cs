using System.Collections.Generic;
using summary.Models;

namespace summary.Services
{
    public interface ISummaryService
    {
        List<Summary> FindAll();

        Summary FindById(int id);
    }
}