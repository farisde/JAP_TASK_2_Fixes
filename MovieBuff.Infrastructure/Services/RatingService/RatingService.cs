using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieBuff.Data;
using MovieBuff.DTOs.Movie;
using MovieBuff.Entities;
using MovieBuff.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBuff.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RatingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// This service method adds a new rating to a movie/tv show. The rating value and media id is sent
        /// through the parameter. It also updates the average rating value of the rated media.
        /// </summary>
        /// <param name="newRating"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<GetRatingDto>> AddMediaRating(AddRatingDto newRating)
        {
            var response = new ServiceResponse<GetRatingDto>();

            if (newRating.Value < 1 || newRating.Value > 5)
            {
                response.Success = false;
                response.Message = "Invalid rating range. Must be between 1 and 5.";
                return response;
            }

            var ratedMedia = await _context.Medias
                .Include(m => m.Cast)
                .Include(m => m.RatingList)
                .FirstOrDefaultAsync(m => m.Id == newRating.RatedMediaId);

            if (ratedMedia == null)
            {
                response.Success = false;
                response.Message = "Selected media ID doesn't exist.";
                return response;
            }

            var rating = new Rating
            {
                Value = newRating.Value,
                RatedMediaId = newRating.RatedMediaId
            };
            _context.Ratings.Add(rating);

            ratedMedia.Rating = ratedMedia.RatingList.Average(r => r.Value);

            _context.Medias.Update(ratedMedia);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetRatingDto>(rating);
            return response;
        }
    }
}
