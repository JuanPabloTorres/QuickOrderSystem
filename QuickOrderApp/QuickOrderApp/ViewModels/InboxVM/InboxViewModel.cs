using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.InboxVM
{
    public class InboxViewModel : BaseViewModel
    {
        public InboxViewModel ()
        {
            UserRequests = new ObservableCollection<RequestPresenter>();

            ExecuteLoadItemsCommand();

            MessagingCenter.Subscribe<RequestPresenter>(this, "RefreshInbox", (sender) =>
            {
                UserRequests.Remove(sender);
            });

            App.ComunicationService.hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                UserRequest deseralized = JsonConvert.DeserializeObject<UserRequest>(message);

                var requestPresenter = new RequestPresenter(deseralized);

                UserRequests.Add(requestPresenter);
            });
        }

        public ObservableCollection<RequestPresenter> UserRequests { get; set; }

        public async Task ExecuteLoadItemsCommand ()
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();
            }
            else
            {
                IsBusy = true;

                try
                {
                    UserRequests.Clear();

                    var requestData = await requestDataStore.GetRequestOfUser(App.LogUser.UserId);

                    foreach( var item in requestData )
                    {
                        if( item.RequestAnswer == Answer.None )
                        {
                            var presenter = new RequestPresenter(item);

                            UserRequests.Add(presenter);
                        }
                    }
                }
                catch( Exception ex )
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}