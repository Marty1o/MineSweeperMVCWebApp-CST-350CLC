using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    public class Cell
    {


        private int id;
        private int x;
        private int y;
        private int liveNeighbors;
        private Boolean visited;
        private Boolean bomb;
        private Boolean isFlagged;



        public Cell()
        {
            id = -1;
            x = -1;
            y = -1;
            liveNeighbors = 0;
            visited = false;
            bomb = false;
            isFlagged = false;
        }

        public Cell(int x, int y)
        {
            id = -1;
            this.x = x;
            this.y = y;
            liveNeighbors = 0;
            visited = false;
            bomb = false;
            isFlagged = false;
        }

        public int Id { get => id; set => id = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int LiveNeighbors { get => liveNeighbors; set => liveNeighbors = value; }
        public bool Visited { get => visited; set => visited = value; }
        public bool Bomb { get => bomb; set => bomb = value; }
        public bool IsFlagged { get => isFlagged; set => isFlagged = value; }   

    }
  //  public Cell GetCell(int x, int y)
  //  {
  //      if (x < 0 || x >= Size || y < 0 || y >= Size) return null;
  //      return Grid[x, y];
  //  }
}