using Highscore.Website.Data;
using Highscore.Website.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Highscore.Website.Areas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GamesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET /api/games
        [HttpGet]
        public IEnumerable<Game> GetAll()
        {
            var games = context.Game.ToList();

            return games;
        }

        // GET /api/games({id}
        [HttpGet("{id}")]
        public ActionResult<Game> Get(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound(); // 404 Not Found
            }

            return game;
        }

        [HttpPost]
        public ActionResult Created(Game game)
        {
            context.Game.Add(game);
            context.SaveChanges();

            return Created($"/api/games/{game.Id}", game); // 201 Created (Location: ...)
            // "Här kan du hitta den nya resursen" och här är resursen så ingen GET behövs
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound(); // 404 Not Found
            }

            context.Game.Remove(game);
            context.SaveChanges();

            return NoContent(); // 204 No Content == resursen har tagits bort
        }
    }
}
