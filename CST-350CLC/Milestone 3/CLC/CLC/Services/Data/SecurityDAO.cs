
using CLC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CLC.Services.Data
{
    public class SecurityDAO
    {

        string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mine2;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public Boolean validUser(LoginRequest loginRequest)
        {
            bool result = false;

            try
            {
                string query = "SELECT * FROM dbo.users WHERE USERNAME=@Username AND PASSWORD=@Password";

                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = loginRequest.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = loginRequest.Password;

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

    }
}