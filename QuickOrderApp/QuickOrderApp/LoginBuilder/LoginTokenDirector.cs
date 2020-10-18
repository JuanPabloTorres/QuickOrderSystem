using Library.DTO;

namespace QuickOrderApp.LoginBuilder
{
    public class LoginTokenDirector
    {
        public TokenDTO MakeLogin(LoginTokenBuilder loginBuilder, string username, string password)
        {
            loginBuilder.CreateLoginToken(username, password);

            loginBuilder.VerifyLogin();

            loginBuilder.MakeHubConnection();

            loginBuilder.GoQuickOrderHome();

            return loginBuilder.GetLogin();
        }
    }
}
