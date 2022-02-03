using System.Collections.Generic;
using api.Models.Entities;

namespace api.Services
{
    public interface ISummaryService
    {
        List<Summary> FindAll();

        Summary FindById(int id);

        Summary Save(Summary summary);

        Summary Update(Summary summary);
    }
}