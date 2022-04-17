using System.Collections.Generic;
using api.Controllers.Dto;
using api.Models.Entities;

namespace api.Services
{
    public interface ISummaryService
    {
        List<Summary> FindAll();

        Summary FindById(int id);

        Summary Save(Summary summary);

        List<TokenizedSummary> FindByMatcher(SummarySearchOptions options);

        Summary Update(Summary summary);

        Summary Delete(int id);
    }
}