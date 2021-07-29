using Library.DTO;

namespace QuickOrderApp.LoginBuilder
{
    public class LoginTokenDirector
    {
        public TokenDTO MakeLogin(LoginTokenBuilder loginBuilder, string username, string password)
        {
            loginBuilder.CreateLoginToken(username, password);

            if (loginBuilder.GetLogin() != null)
            {
                loginBuilder.VerifyLogin();

                loginBuilder.MakeHubConnection();

                return loginBuilder.GetLogin();

            }
            else
            {
                loginBuilder.ErrorMessage();
                return null;
            }
        }
    }
}
