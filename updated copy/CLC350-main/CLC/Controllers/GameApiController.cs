using CLC.Models;
using CLC.Services.Business.Game;
using CLC.Services.Data.Game;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CLC.Controllers
{
    [ApiController]
    [Route("api")]
    public class GameApiController : ControllerBase
    {
        // Game Service and Save Game DAO
        private GameDAO _dao;
        private GameService _gameService = new GameService();

        // Intiate a new DAO
        public GameApiController()
        {
            _dao = new GameDAO();
        }

        // Returns all the games located in the DAO
        [HttpGet("savedGames")]
        public IEnumerable<SavedGame> GetAllSavedGames()
        {
            return _dao.AllGames();
        }

      //  [HttpGet("savedGames/{id}")]
      //  public ActionResult<SavedGame> GetSavedGameById(int id)
      //  {
      //      return _dao.GetGameById(id);
      //  }

        // Deletes a game from the DAO
      //  [HttpDelete("savedGames/{id}")]
      //  public StatusCodeResult DeleteGameById(int id)
      //  {

      //      return _dao.DeleteGameById(id) == 0 ?
      //          StatusCode(204)
       //         :
       //         StatusCode(500);
       // }

    }
}