using MovieBuff.DTOs.Reporting;
using MovieBuff.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBuff.Services.ReportingService
{
    public interface IReportingService
    {
        Task<ServiceResponse<List<Top10MoviesByRatingResult>>> Top10MoviesByRating();
        Task<ServiceResponse<List<Top10MoviesByScreeningsForPeriodResult>>> Top10MoviesByScreeningsForPeriod(GetScreeningPeriodDto screeningPeriod);
        Task<ServiceResponse<List<MostSoldMoviesWithoutRatingsResult>>> MostSoldMoviesWithoutRatings();
    }
}