using MovieBuff.DTOs.Ticket;
using System;
using System.Collections.Generic;

namespace MovieBuff.DTOs.Screening
{
    public class GetScreeningDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public float Duration { get; set; }
        public int AvailableSeats { get; set; }
        public int ReservedSeats { get; set; }
        public List<GetTicketDto> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}