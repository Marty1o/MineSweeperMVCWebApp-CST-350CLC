using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    public class SavedGame
    {
        [DisplayName("Id number")]
        public int id { get; set; }
        public int gridId { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public DateTime date { get; set; }

        public SavedGame()
        {

        }
        public SavedGame(int id, int gridId, int rows, int cols, DateTime date)
        {
            this.id = id;
            this.gridId = gridId;
            this.rows = rows;
            this.cols = cols;
            this.date = date;
        }
    }
}