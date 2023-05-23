using CLC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CLC.Services.Data.Game
{
    public class GameDAO
    {

        string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minesweeper1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Grid findGrid(User user)
        {
            Grid g = null;
            try
            {
                
                string query = "SELECT * FROM dbo.grids WHERE USERID=@id";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = user.Id;
  

                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int rows = int.Parse(reader["ROWS"].ToString());
                        int cols = int.Parse(reader["COLS"].ToString());
                        int USER_ID = int.Parse(reader["USERID"].ToString());
                        Boolean GAMEOVER = Boolean.Parse(reader["GAMEOVER"].ToString());
                    
                        g = new Grid(ID, rows, cols, USER_ID, GAMEOVER);
                        g.Cells = new Cell[cols, rows];
                    }

                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }


            if (g != null)
            {
                try
                {
                    string query = "SELECT * FROM dbo.cells WHERE GRIDID=@id";

                    using (SqlConnection cn = new SqlConnection(conn))
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = g.Id;


                        cn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int ID = int.Parse(reader["ID"].ToString());
                            int x = int.Parse(reader["X"].ToString());
                            int y = int.Parse(reader["Y"].ToString());
                            Boolean bomb = Boolean.Parse(reader["BOMB"].ToString());
                            Boolean visited = Boolean.Parse(reader["VISITED"].ToString());
                            int live = int.Parse(reader["LIVENEIGHBORS"].ToString());
                            int gridId = int.Parse(reader["GRIDID"].ToString());

                            Cell c = new Cell(x, y);
                            c.Id = ID;
                            c.Bomb = bomb;
                            c.Visited = visited;
                            c.LiveNeighbors = live;
                            g.Cells[x, y] = c;

                        }

                        cn.Close();
                    }

                }
                catch (SqlException e)
                {
                    throw e;
                }
            }

            return g;


        }


        public void createGrid(Grid grid)
        {

            int gridID = -1;
            try
            {
                string query = "INSERT INTO dbo.grids (ROWS, COLS, USERID, GAMEOVER) " +
                    "VALUES (@Rows, @Cols, @User_ID, @GameOver) SELECT SCOPE_IDENTITY()";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;


                  
                    cn.Open();
                    gridID = Convert.ToInt32(cmd.ExecuteScalar());

                    cn.Close();


                   
                }

            }
            catch (SqlException e)
            {
                throw e;
            }


            try
            {
                string query = "INSERT INTO dbo.cells (X, Y, BOMB, VISITED, LIVENEIGHBORS, GRIDID) " +
                    "VALUES (@x, @y, @bomb, @visited, @live, @grid)";

             
                for (int y = 0; y < grid.Rows; y++) {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(conn))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                           
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x,y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = gridID;

                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();

               

                        }
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }



        }



        public void updateGrid(Grid grid)
        {

            try
            {

                string query = "UPDATE dbo.grids SET ROWS = @Rows, COLS = @Cols, USERID = @User_ID, GAMEOVER = @GameOver WHERE ID=@id";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Id;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    cn.Close();



                }

            }
            catch (SqlException e)
            {
                throw e;
            }


            try
            {

                string query = "UPDATE dbo.cells SET X = @x, Y = @y, BOMB = @bomb, VISITED = @visited, LIVENEIGHBORS = @live, " +
                    "GRIDID = @grid WHERE ID=@id";



                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(conn))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = grid.Id;
                            cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Cells[x, y].Id;
                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();

                        }
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }



        }




        public void deleteGrid(User user)
        {

            try
            {
                string query = "DELETE FROM dbo.grids WHERE USERID=@Id ";


                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


}

    }
}