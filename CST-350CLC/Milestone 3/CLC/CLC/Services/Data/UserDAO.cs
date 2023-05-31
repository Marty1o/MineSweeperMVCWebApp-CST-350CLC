using CLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace CLC.Services.Data
{
    public class UserDAO
    {


        string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mine2;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public User findUser(LoginRequest loginRequest)
        {
            User user = null;
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
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        String username = reader["USERNAME"].ToString();
                        String password = reader["PASSWORD"].ToString();
                        String email = reader["EMAIL"].ToString();
                        String firstname = reader["FIRSTNAME"].ToString();
                        String lastname = reader["LASTNAME"].ToString();
                        String sex = reader["SEX"].ToString();
                        int age = int.Parse(reader["AGE"].ToString());
                        String state = reader["STATE"].ToString();

                        user = new User(ID, username, password, email, firstname, lastname, sex, age, state);


                            
                    }

                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return user;
        }



    }
}