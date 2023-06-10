using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    public class SavedGame
    {
        public int id { get; set; }
        public int gridId { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }

        public SavedGame()
        {

        }
        public SavedGame(int id, int gridId, int rows, int cols)
        {
            this.id = id;
            this.gridId = gridId;
            this.rows = rows;
            this.cols = cols;
        }
    }
}