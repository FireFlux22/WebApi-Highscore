using System;

namespace Highscore.Website.Data.Entities
{
    public class Score
    {
        public Score(int id, uint points, DateTime date, int playerId, int gameId)
        {
            Id = id;
            Points = points;
            Date = date;
            PlayerId = playerId;
            GameId = gameId;
        }

        public Score()
        {

        }

        public int Id { get; set; }
        public uint Points { get; set; }
        public DateTime Date { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public Player Player { get; set; }

    }
}