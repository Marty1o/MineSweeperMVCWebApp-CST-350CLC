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


        [HttpGet]
        public ActionResult resetGrid()
        {

            GameService gameService = new GameService();
            gameService.removeGrid(this);

            //returns view
            return Index();



        }
    }
}