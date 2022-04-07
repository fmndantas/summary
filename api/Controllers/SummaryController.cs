using api.Controllers.Dto;
using api.Models.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SummaryController : ControllerBase
    {
        private ISummaryService _summaryService;
        private ILogger<SummaryController> _logger;

        public SummaryController(ISummaryService summaryService, ILogger<SummaryController> logger)
        {
            _summaryService = summaryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            return Ok(_summaryService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            return Ok(_summaryService.FindById(id));
        }

        [HttpPost]
        public IActionResult Save([FromBody] Summary summary)
        {
            return Ok(_summaryService.Save(summary));
        }

        [HttpPost("matcher")]
        public IActionResult FindByMatcher([FromBody] SummarySearchOptions options)
        {
            return Ok(_summaryService.FindByMatcher(options));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Summary summary)
        {
            return Ok(_summaryService.Update(summary));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_summaryService.Delete(id));
        }
    }
}