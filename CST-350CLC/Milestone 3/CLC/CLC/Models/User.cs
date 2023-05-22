using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC.Models
{
    [Serializable]
    public class User
    {

        private int id;
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private string sex;
        private int age;
        private string state;


        public User()
        {
            this.id = -1;
            this.username = "";
            this.password = "";
            this.email = "";
            this.firstName = "";
            this.lastName = "";
            this.sex = "";
            this.age = 0;
            this.state = "";

        }

        public User(int id, string username, string password, string email, string firstName, string lastName, string sex, int age, string state)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.age = age;
            this.state = state;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public string State { get => state; set => state = value; }

    }
}