using CLC.Models;
using CLC.Services.Data.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC.Services.Business.Game
{
    public class GameService
    {


        public Grid findGrid(Controller c)
        {
            User user = (User)c.Session["user"];


            GameDAO gameDAO = new GameDAO();

            return gameDAO.findGrid(user);

        }

        public void removeGrid(Controller c)
        {
            User user = (User)c.Session["user"];

            GameDAO gameDAO = new GameDAO();

            gameDAO.deleteGrid(user);

        }

        public void activateCell(Grid g, int X, int Y)
        {


            GameDAO gameDAO = new GameDAO();

            Cell c = g.Cells[X, Y];

            c.Visited = true;

            if (c.Bomb)
            {
                for (int y = 0; y < g.Rows; y++)
                {
                    for (int x = 0; x < g.Cols; x++)
                    {
                        g.Cells[x, y].Visited = true;
                    }
                }
                System.Diagnostics.Debug.WriteLine("Hit bomb at: " + X + ", " + Y);
            }
            else
            {
                if (c.LiveNeighbors == 0)
                    revealSurroundingCells(g, c.X, c.Y);

            }


            gameDAO.updateGrid(g);

        }

        private void revealSurroundingCells(Grid g, int x, int y)
        {
            RevealNextCell(g, x - 1, y - 1);
            RevealNextCell(g, x - 1, y);
            RevealNextCell(g, x - 1, y + 1);
            RevealNextCell(g, x + 1, y);
            RevealNextCell(g, x, y - 1);
            RevealNextCell(g, x, y + 1);
            RevealNextCell(g, x + 1, y - 1);
            RevealNextCell(g, x + 1, y + 1);
        }

        private void RevealNextCell(Grid g, int x, int y)
        {

            if (!(x >= 0 && x < g.Cols && y >= 0 && y < g.Rows)) return;

            if (g.Cells[x, y].Visited) return;

            if (g.Cells[x, y].LiveNeighbors == 0)
            {
                g.Cells[x, y].Visited = true;
                revealSurroundingCells(g, x, y);
            }

            else if (!g.Cells[x, y].Bomb)
            {
                g.Cells[x, y].Visited = true;
            }

        }


        public Grid createGrid(Controller c, int width, int height)
        {
            User user = (User)c.Session["user"];

            Grid grid = new Grid(-1, width, height, user.Id, false);
            Cell[,] cells = new Cell[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }

            Random rand = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (rand.Next(0, 100) <= 10)
                    {
                        cells[x, y].Bomb = true;
                        cells[x, y].LiveNeighbors = 9;
                        for (int neighborX = -1; neighborX <= 1; neighborX++)
                        {
                            for (int neighborY = -1; neighborY <= 1; neighborY++)
                            {
                                if (neighborX == 0 && neighborY == 0)
                                {

                                }
                                else if (x + neighborX >= 0 && x + neighborX < width && y + neighborY >= 0 && y + neighborY < height)
                                {
                                    cells[x + neighborX, y + neighborY].LiveNeighbors++;
                                }

                            }
                        }

                    }
                }
            }
            grid.Cells = cells;




            GameDAO gameDAO = new GameDAO();

            gameDAO.createGrid(grid);

            
            return grid;
        }
        
    }
}