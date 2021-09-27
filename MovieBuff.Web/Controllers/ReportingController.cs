using Microsoft.AspNetCore.Mvc;
using MovieBuff.DTOs.Reporting;
using MovieBuff.Models;
using MovieBuff.Services.ReportingService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JAP_TASK_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService _reportingService;

        public ReportingController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpGet("topTenMoviesByRating")]
        public async Task<ActionResult<ServiceResponse<List<Top10MoviesByRatingResult>>>> GetTop10MoviesByRating()
        {
            var response = await _reportingService.Top10MoviesByRating();
            return Ok(response);
        }

        [HttpPost("topTenMoviesByScreeningsForPeriod")]
        public async Task<ActionResult<ServiceResponse<List<Top10MoviesByScreeningsForPeriodResult>>>> Top10MoviesByScreeningsForPeriod(GetScreeningPeriodDto screeningPeriod)
        {
            var response = await _reportingService.Top10MoviesByScreeningsForPeriod(screeningPeriod);
            return Ok(response);
        }

        [HttpGet("mostSoldMoviesWithoutRatings")]
        public async Task<ActionResult<ServiceResponse<List<Top10MoviesByRatingResult>>>> GetMostSoldMoviesWithoutRatings()
        {
            var response = await _reportingService.MostSoldMoviesWithoutRatings();
            return Ok(response);
        }
    }
}