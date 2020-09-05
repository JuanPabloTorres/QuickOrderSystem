using Library.DTO;
using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
   public  class UserCardsViewModel:BaseViewModel
    {

        public ObservableCollection<PaymentCardPresenter> PaymentCardPresenters { get; set; }

        public UserCardsViewModel()
        {
            PaymentCardPresenters = new ObservableCollection<PaymentCardPresenter>();

            ExecuteLoadItems();

            MessagingCenter.Subscribe<PaymentCardPresenter>(this, "PaymencardDeleteMsg", (sender) => 
            {
                PaymentCardPresenters.Remove(sender);
            });
        }


        async Task ExecuteLoadItems()
        {

            var tokenData = new  JwtSecurityTokenHandler().ReadJwtToken(App.TokenDto.Token);
            var cardData = await CardDataStore.GetCardDTOFromUser(App.LogUser.UserId,App.TokenDto.Token);

            foreach (var item in cardData)
            {
                var cardPresenter = new PaymentCardPresenter(item);

                PaymentCardPresenters.Add(cardPresenter);
            }
        }

    }
}
