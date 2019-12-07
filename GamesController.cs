﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevGames.Models;
using DevGames.Services;

namespace DevGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<List<Games>>> Get()
        {
            return await _gameService.Get();
        }

        // GET: api/Games/5
        [HttpGet("{id}", Name = "GetGame")]
        public async Task<ActionResult<Games>> GetById(string id)
        {
            return await _gameService.GetById(id);
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<Games>> Create(Games game)
        {
            if (ModelState.IsValid)
            {
                await _gameService.Create(game);
                return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
            }

            return BadRequest();
        }

        // PUT: api/Games/5
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Games game)
        {
            var gameExist = await _gameService.GetById(id);

            if (gameExist == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                await _gameService.Update(id, game);
                return OK();
            }

            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id, Games game)
        {
            var gameExist = await _gameService.GetById(id);

            if (gameExist == null)
            {
                return NotFound();
            }

            await _gameService.Delete(id);

            return NoContent();

        }
    }
}
