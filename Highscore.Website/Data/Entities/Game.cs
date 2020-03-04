namespace Highscore.Website.Data.Entities
{
    public class Game
    {
        public Game(int id, string title, string description, string imageUrl)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
        }

        public Game() 
        { 

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // ändra från URI till string

       // public IList<Score> Scores { get; set; } = new List<Score>();
    }
}
