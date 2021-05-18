namespace OpenHack.Entities
{
    public class RatingType
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Timestamp { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNote { get; set; }
    }
}