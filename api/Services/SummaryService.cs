using System.Collections.Generic;
using System.Linq;
using api.Models;
using api.Models.Entities;

namespace api.Services
{
    public class SummaryService : ISummaryService
    {
        private IAbstractRepository<Summary> _repository;

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

        public Summary Save(Summary summary)
        {
            _repository.Add(summary);
            return summary;
        }
    }
}