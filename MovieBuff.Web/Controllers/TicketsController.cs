using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBuff.Core.Services.TicketService;
using MovieBuff.DTOs.Screening;
using MovieBuff.DTOs.Ticket;
using MovieBuff.Models;
using System.Threading.Tasks;

namespace MovieBuff.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("BuyTickets")]
        public async Task<ActionResult<ServiceResponse<GetScreeningDto>>> BuyTicketsForScreening(BuyTicketDto buyTicket)
        {
            var response = await _ticketService.BuyTicketsForScreening(buyTicket);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
