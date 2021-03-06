﻿using Highscore.Website.Data;
using Highscore.Website.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Highscore.Website.Areas.API.Controllers
{


    [Authorize]
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
        public ActionResult<Game> GetById(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound(); // 404 Not Found
            }

            return game;
        }

        [Authorize(Policy = "IsAdministrator")]
        [HttpPost]
        public ActionResult Created(Game game)
        {
            context.Game.Add(game);
            context.SaveChanges();

            // return Created($"/api/games/{game.Id}", game); // 201 Created (Location: ...)
            // "Här kan du hitta den nya resursen" och här är resursen så ingen GET behövs
            
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game); // samma resultat
        }

        [Authorize(Policy = "IsAdministrator")]
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

        // vid en PUT måste du skicka med all information igen
        [Authorize(Policy = "IsAdministrator")]
        [HttpPut("{id}")]
        public ActionResult Replace(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest(new { reason = "Game id does not match" }); // 400 Bad Request
            }

            context.Entry(game).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch 
            {
                if (!context.Game.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content
        }

        // Install-Package Microsoft.AspNetCore.JsonPatch
        // Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson

        // PATCH /api/games/2
        [HttpPatch("{id}")]
        public ActionResult Update(int id, JsonPatchDocument<Game> patchDoc)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(game);

            context.SaveChanges();

            return NoContent();
        }
    }
}
