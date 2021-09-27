namespace MovieBuff.Models
{
    public class Top10MoviesByRatingResult
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public int RatingsCount { get; set; }
        public double MovieRating { get; set; }
    }
}