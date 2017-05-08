using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Info_Banks_of_Moscow
{
    class User
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public User(string login)
        {        
            _login = login;
        }

        public User(string name, string login)
        {
            _name = name;        
            _login = login;            
        }
    }
}
