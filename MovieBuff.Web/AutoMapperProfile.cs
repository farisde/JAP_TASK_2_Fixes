using AutoMapper;
using MovieBuff.DTOs.Movie;
using MovieBuff.DTOs.Screening;
using MovieBuff.DTOs.Ticket;
using MovieBuff.Entities;

namespace MovieBuff
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Media, GetMediaDto>();
            CreateMap<CastMember, GetCastMemberDto>();
            CreateMap<Rating, GetRatingDto>();
            CreateMap<AddRatingDto, Rating>();
            CreateMap<Screening, GetScreeningDto>();
            CreateMap<Ticket, GetTicketDto>();
        }
    }
}