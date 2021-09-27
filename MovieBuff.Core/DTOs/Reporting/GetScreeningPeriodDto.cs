using System;

namespace MovieBuff.DTOs.Reporting
{
    public class GetScreeningPeriodDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}