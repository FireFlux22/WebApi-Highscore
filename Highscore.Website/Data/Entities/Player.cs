namespace Highscore.Website.Data.Entities
{
    public class Player
    {
        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Player()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}