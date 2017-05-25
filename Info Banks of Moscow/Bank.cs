using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Info_Banks_of_Moscow
{
    [Serializable]
    class Bank
    {        
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _metro;

        public string Metro
        {
            get { return _metro; }
            set { _metro = value; }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }


        private string _rate;

        public string Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        private string _opinion;

        public string Opinion
        {
            get { return _opinion; }
            set { _opinion = value; }
        }

        //public string OpinionFormer (Bank Bank)
        //{
        //    List<User> Users = new List<User>(); 
        //    FileStream fs = new FileStream("name.txt", FileMode.Open);
        //    using (StreamReader sr = new StreamReader(fs))
        //    {
        //        string str = sr.ReadLine();
        //        string[] loginname = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        User AuthorisedUser = new User(loginname[1], loginname[0]);
        //        Users.Add(AuthorisedUser);
        //    }
        //    return string.Format("[{0}]: {1}", Users[0].Login, Bank.Opinion);
        //}

        public string GetUserName(Bank Bank)
        {
            string str = Bank.Opinion;
            string[] username = str.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            return username[0];
        }

        public string GetJustOpinion(Bank Bank)
        {
            string str = Bank.Opinion;
            string[] username = str.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            char[] opinionLetters = username[1].ToCharArray();

            for (int i = 0; i < opinionLetters.Length-1; i++)
            {
                opinionLetters[i] = opinionLetters[i + 1];
            }

            string justOpinion = "";
            for (int i = 0; i < opinionLetters.Length-1; i++)
            {
                justOpinion = justOpinion + opinionLetters[i];
            }
           

            return justOpinion;
        }

        public Bank(string name, string address, string metro, string telephone, string rate, string opinion)
        {
            _name = name;
            _address = address;
            _metro = metro;
            _telephone = telephone;
            _rate = rate;
            List<User> users = new List<User>();
            FileStream fs = new FileStream("name.txt", FileMode.Open);
            using (StreamReader sr = new StreamReader(fs))
            {
                string str = sr.ReadLine();
                string[] loginName = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                User authorisedUser = new User(loginName[1], loginName[0]);
                users.Add(authorisedUser);
            }
            _opinion = string.Format("[{0}]: {1}", users[0].Login, opinion);
           
        }
                
    }
}
