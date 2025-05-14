namespace BaoDienTu_ASPNET.Models
{
    public class NewsRelation
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public int RelatedNewsId { get; set; }
        public News RelatedNews { get; set; }
    }
}
