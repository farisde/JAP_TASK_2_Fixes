using MovieBuff.DTOs.Screening;
using MovieBuff.DTOs.Ticket;
using MovieBuff.Models;
using System.Threading.Tasks;

namespace MovieBuff.Core.Services.TicketService
{
    public interface ITicketService
    {
        Task<ServiceResponse<GetScreeningDto>> BuyTicketsForScreening(BuyTicketDto buyTicket);
    }
}
