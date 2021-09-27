using System;
using System.Collections.Generic;

namespace MovieBuff.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        //duration is in minutes
        public float Duration { get; set; }
        public int AvailableSeats { get; set; }
        public int ReservedSeats { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Media Media { get; set; }
        public int MediaId { get; set; }
    }
}