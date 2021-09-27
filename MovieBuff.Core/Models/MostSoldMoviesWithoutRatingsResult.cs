namespace MovieBuff.Models
{
    public class MostSoldMoviesWithoutRatingsResult
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string ScreeningName { get; set; }
        public int TicketsSold { get; set; }
    }
}
