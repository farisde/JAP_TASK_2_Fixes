using Microsoft.EntityFrameworkCore;
using MovieBuff.Data;
using MovieBuff.DTOs.Reporting;
using MovieBuff.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBuff.Services.ReportingService
{
    public class ReportingService : IReportingService
    {
        private readonly DataContext _context;

        public ReportingService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<MostSoldMoviesWithoutRatingsResult>>> MostSoldMoviesWithoutRatings()
        {
            var response = new ServiceResponse<List<MostSoldMoviesWithoutRatingsResult>>();
            response.Data = await _context.MostSoldMoviesWithoutRatingsResults
                                          .FromSqlRaw("EXEC [dbo].[MostSoldMoviesWithoutRatings];")
                                          .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<Top10MoviesByRatingResult>>> Top10MoviesByRating()
        {
            var response = new ServiceResponse<List<Top10MoviesByRatingResult>>();
            response.Data = await _context.Top10MoviesByRatingResults
                                          .FromSqlRaw("EXEC [dbo].[Top10MoviesByRating];")
                                          .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<Top10MoviesByScreeningsForPeriodResult>>> Top10MoviesByScreeningsForPeriod(GetScreeningPeriodDto screeningPeriod)
        {
            var response = new ServiceResponse<List<Top10MoviesByScreeningsForPeriodResult>>();
            response.Data = await _context.Top10MoviesByScreeningsForPeriodResults
                                          .FromSqlRaw("EXEC [dbo].[Top10MoviesByScreeningsForPeriod] {0}, {1};", screeningPeriod.StartDate, screeningPeriod.EndDate)
                                          .ToListAsync();
            return response;
        }
    }
}