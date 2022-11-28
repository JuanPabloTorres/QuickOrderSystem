using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class UserCardsViewModel : BaseViewModel
    {
        public UserCardsViewModel ()
        {
            PaymentCardPresenters = new ObservableCollection<PaymentCardPresenter>();

            ExecuteLoadItems();

            MessagingCenter.Subscribe<PaymentCardPresenter>(this, "PaymencardDeleteMsg", (sender) =>
            {
                PaymentCardPresenters.Remove(sender);
            });
        }

        public ObservableCollection<PaymentCardPresenter> PaymentCardPresenters { get; set; }

        private async Task ExecuteLoadItems ()
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();
            }
            else
            {
                var tokenData = new JwtSecurityTokenHandler().ReadJwtToken(App.TokenDto.Token);

                var cardData = await CardDataStore.GetCardDTOFromUser(App.LogUser.ID, App.TokenDto.Token);

                foreach( var item in cardData )
                {
                    var cardPresenter = new PaymentCardPresenter(item);

                    PaymentCardPresenters.Add(cardPresenter);
                }
            }
        }
    }
}