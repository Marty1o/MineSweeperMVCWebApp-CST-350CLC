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
            //create user service
            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {

                //create game service object
                GameService gameService = new GameService();

                //load grid for user
                Grid g = gameService.findGrid(this);

                //check if user has an existing grid saved in db
                if (g != null)
                {
                    

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
                //user isn't logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }


        //cell click form handle
        [HttpPost]
        public ActionResult activateCell(string id, string x, string y)
        {

            //create userservice
            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {
                //update cell components
                GameService gameService = new GameService();

                //load user grid from db
                Grid g = gameService.findGrid(this);

                //activate cell logic
                gameService.activateCell(g, int.Parse(x), int.Parse(y));

                //return same view
                return Index();
            }
            else
            {
                //user not logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }


        [HttpGet]
        public ActionResult resetGrid()
        {
            //deletes grid from db

            GameService gameService = new GameService();
            gameService.removeGrid(this);

            //returns view
            return Index();



        }
    }
}