using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using summary.Models;
using summary.Services;

namespace summary.Controllers
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
    }
}