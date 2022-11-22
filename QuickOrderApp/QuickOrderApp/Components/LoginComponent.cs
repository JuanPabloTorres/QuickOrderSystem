using Library.Services.Interface;
using Xamarin.Forms;

namespace QuickOrderApp.Components
{
    public class LoginComponent
    {
        private string password;

        private string username;

        public LoginComponent (string _username, string _password)
        {
            this.Username = _username;

            this.Password = _password;
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        //public bool OnLogin()
        //{
        //}
    }
}