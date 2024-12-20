using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr_4_5
{
    internal class User
    {
        public int id { get; set; }
        private string phone_number, password;
        
        public string Password 
        { 
            get
            {
                return password;
            } 
            set
            {
                password = value; 
            }
        }

        public string Phone_number
        {
            get
            {
                return phone_number;
            }
            set
            {
                phone_number = value;
            }
        }

          public User() { }

        public User(string phone_number, string password)
        {
            this.phone_number = phone_number;
            this.password = password;
        }

    }
}
