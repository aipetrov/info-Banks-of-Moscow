using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Info_Banks_of_Moscow
{
    static class Pages
    {
        private static ProfilePage _profilePage = new ProfilePage();
        private static LoginPage _loginPage = new LoginPage();
        private static AddBankPage _addBankPage = new AddBankPage();
        private static ShowBanksPage _showBanksPage = new ShowBanksPage();
        private static RegisterPage _registerPage = new RegisterPage();
        private static FindBankPage _findBankPage = new FindBankPage();
        private static DeleteBankPage _deleteBankPage = new DeleteBankPage();
        private static GuestPage _guestPage = new GuestPage();

        public static LoginPage LoginPage
        {
            get
            {
                return _loginPage;
            }
        }

        public static GuestPage GuestPage
        {
            get
            {
                return _guestPage;
            }
        }

        public static DeleteBankPage DeleteBankPage
        {
            get
            {
                return _deleteBankPage;
            }
        }


        public static FindBankPage FindBankPage
        {
            get
            {
                return _findBankPage;
            }
        }

        public static RegisterPage RegisterPage
        {
            get
            {
                return _registerPage;
            }
        }

        public static ShowBanksPage ShowBanksPage
        {
            get
            {
                return _showBanksPage;
            }
        }

        public static ProfilePage ProfilePage
        {
            get
            {
                return _profilePage;
            }
        }

        public static AddBankPage AddBankPage
        {
            get
            {
                return _addBankPage;
            }
        }
    }
}
