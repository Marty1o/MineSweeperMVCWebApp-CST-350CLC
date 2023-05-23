using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

using CLC.Models;

namespace CLC.Services.Data
{
    public class RegisterSecurityDAO
    {

        string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minesweeper1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool userExists(RegisterRequest registerRequest)
        {
            bool result = false;

            try
            {
                string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = registerRequest.Username;

                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        result = true;
                    else
                        result = false;

                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return result;
        }

        public bool emailExists(RegisterRequest registerRequest)
        {
            bool result = false;

            try
            {
                string query = "SELECT * FROM dbo.users WHERE EMAIL=@Email";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = registerRequest.Email;

                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        result = true;
                    else
                        result = false;

                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return result;
        }

        public bool createUser(RegisterRequest registerRequest)
        {
            bool result = false;

            try
            {
                string query = "INSERT INTO dbo.Users (USERNAME, PASSWORD, EMAIL, FIRSTNAME, LASTNAME, SEX, AGE, STATE) " +
                    "VALUES (@Username, @Password, @Email, @FirstName, @LastName, @Sex, @Age, @State)";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = registerRequest.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 20).Value = registerRequest.Password;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = registerRequest.Email;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 20).Value = registerRequest.FirstName ?? "N/A";
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 20).Value = registerRequest.LastName ?? "N/A";
                    cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 20).Value = registerRequest.Sex ?? "N/A";
                    cmd.Parameters.Add("@Age", SqlDbType.Int, 11).Value = registerRequest.Age ?? 0;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 20).Value = registerRequest.State ?? "N/A";

                    cn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                        result = true;
                    else
                        result = false;
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return result;
        }
    }
}