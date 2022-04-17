using System.Collections.Generic;
using System.Linq;
using api.Controllers.Dto;
using api.Models;
using api.Models.Entities;
using api.Models.Search;
using api.Models.Search.Factory;

namespace api.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IAbstractRepository<Summary> _repository;

        public SummaryService(IAbstractRepository<Summary> repository)
        {
            _repository = repository;
        }

        public List<Summary> FindAll()
        {
            return _repository.FindAll();
        }

        public Summary FindById(int id)
        {
            return _repository
                .FindAll()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<TokenizedSummary> FindByMatcher(SummarySearchOptions options)
        {
            var result = new List<TokenizedSummary>();
            var tokenizer = new StringStringTokenizerFactory()
                .MakeTokenizer(new TokenizerFactoryOptions(options));
            foreach (var summary in FindAll())
            {
                var tokenizedTitleSummary = new TokenizedSummary(summary, options, tokenizer);
                if (tokenizedTitleSummary.HasAnyResult)
                {
                    result.Add(tokenizedTitleSummary);
                }
            }

            return result;
        }

        public Summary Save(Summary summary)
        {
            _repository.Add(summary);
            return summary;
        }

        public Summary Update(Summary summary)
        {
            _repository.Update(summary);
            return summary;
        }

        public Summary Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}