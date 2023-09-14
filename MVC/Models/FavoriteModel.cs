#nullable disable

using Microsoft.CodeAnalysis.Options;

namespace MVC.Models
{
    public class FavoriteModel
    {
        public int BlogId { get; set; }

        public int UserId { get; set; }

        public string BlogTitle { get; set; }

        public double BlogScore { get; set; }

        public FavoriteModel(int blogId, int userId, string blogTitle, double blogScore)
        {
            BlogId = blogId;
            UserId = userId;
            BlogTitle = blogTitle;
            BlogScore = blogScore;
        }
    }
}
