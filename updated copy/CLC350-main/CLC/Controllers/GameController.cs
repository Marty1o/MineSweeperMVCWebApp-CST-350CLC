using CLC.Models;
using CLC.Services.Business;
using CLC.Services.Business.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            UserService userService = new UserService();

            if (userService.loggedIn(this))
            {

                
                GameService gameService = new GameService();

                Grid g = gameService.findGrid(this);

                if (g != null)
                {
                    // todo

                }
                else
                {
                    //generate a grid for user
                    g = gameService.createGrid(this, 10, 10);
                }


                //return game board view with grid model
                return View("Game", g);

            }

            else
            {
                //user not logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }
        
        [HttpPost]
        public ActionResult activateCell(string id, string x, string y)
        {

            
            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {
                //update cell 
                GameService gameService = new GameService();

                //load user grid 
                Grid g = gameService.findGrid(this);

                //activate cell 
                gameService.activateCell(g, int.Parse(x), int.Parse(y));

                //return same view
                return PartialView("GameBoard", g);
            }
            else
            {
                
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult activateFlag(string id, string x, string y)
        {


            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {
                //update cell 
                GameService gameService = new GameService();

                //load user grid 
                Grid g = gameService.findGrid(this);

                //activate cell 
                gameService.activateFlag(g, int.Parse(x), int.Parse(y));

                //return same view
                return PartialView("GameBoard", g);
            }
            else
            {

                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }


        [HttpPost]
        public ActionResult deactivateFlag(string id, string x, string y)
        {


            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {
                //update cell 
                GameService gameService = new GameService();

                //load user grid 
                Grid g = gameService.findGrid(this);

                //activate cell 
                gameService.deactivateFlag(g, int.Parse(x), int.Parse(y));

                //return same view
                return PartialView("GameBoard", g);
            }
            else
            {

                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }


        [HttpGet]
        public ActionResult resetGrid()
        {

            GameService gameService = new GameService();
            gameService.removeGrid(this);

            //returns view
            return Index();
        }

        [HttpPost]
        public ActionResult onSaveButton()
        {
            GameService service = new GameService();
            service.saveGame(this);
            service.removeGrid(this);
            return Index();
        }

        [HttpGet]
        public ActionResult onLoadButton()
        {
            GameService service = new GameService();
            List<SavedGame> savedGames;
            savedGames = service.loadGames(this);
            return View(savedGames);
        }

        [HttpPost]
        public ActionResult deleteSavedGame(int id)
        {
            GameService gameService = new GameService();
            gameService.deleteSavedGame(id);

            // Redirect to the desired action or view
            return RedirectToAction("Index");
        }

        public ActionResult PlaySavedGame(int userID, int gridID, int rows, int cols)
        {
            GameService gameService = new GameService();
            gameService.playSavedGame(userID, gridID, rows, cols);

            Grid g = gameService.findGrid(this);


            //return game board view with grid model
            return View("Game", g);
        }
    }
}