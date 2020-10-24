using Library.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using QuickOrderApp.Managers;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.InboxVM
{
    public class InboxViewModel : BaseViewModel
    {

        public Dictionary<string,ContentView> InboxViewTemplatesDictionary { get; set; }
        public ObservableCollection<MessageInboxPresenter> UserRequests { get; set; }
        public ObservableCollection<ContentView> InboxViewTemplates { get; set; }

        public InboxViewModel()
        {
            UserRequests = new ObservableCollection<MessageInboxPresenter>();

            InboxViewTemplates = new ObservableCollection<ContentView>();

            InboxViewTemplatesDictionary = new Dictionary<string, ContentView>();

            ExecuteLoadItemsCommand();

            MessagingCenter.Subscribe<MessageInboxPresenter>(this, "RefreshInbox", (sender) =>
            {

                if (InboxViewTemplatesDictionary.Count() > 0)
                {

                    if (InboxViewTemplatesDictionary.ContainsKey(sender.RequestId.ToString()))
                    {

                        InboxViewTemplates.Remove(InboxViewTemplatesDictionary[sender.RequestId.ToString()]);

                        InboxViewTemplatesDictionary.Remove(sender.RequestId.ToString());


                    }

                }

            });


            App.ComunicationService.hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                UserRequest deseralized = JsonConvert.DeserializeObject<UserRequest>(message);

                var requestPresenter = new MessageInboxPresenter(deseralized);

                UserRequests.Add(requestPresenter);
            });
        }


        public ContentView SetInboxTemplate(UserRequest request)
        {
            ContentView viewtemplate;

            MessageInboxPresenter messageInboxPresenter = new MessageInboxPresenter(request);

            if (request.Type == RequestType.StoreLicensesRequest)
            {

              return  viewtemplate = new StoreLicenceTemplate(messageInboxPresenter);
                
            }

            if (request.Type == RequestType.JobRequest)
            {
               return  viewtemplate = new JobRequestTemplate(messageInboxPresenter);
               
            }

            return null;

        }

        public async Task ExecuteLoadItemsCommand()
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);
            if (tokenExpManger.IsExpired())
            {
                await tokenExpManger.CloseSession();
            }
            else
            {
                IsBusy = true;

                try
                {

                    if (InboxViewTemplatesDictionary.Count() > 0)
                    {
                        InboxViewTemplates.Clear();

                    }

                    InboxViewTemplates.Clear();


                    var requestData = await requestDataStore.GetRequestOfUser(App.LogUser.UserId);

                    foreach (var item in requestData)
                    {

                        var _template = SetInboxTemplate(item);

                        if (_template != null)
                        {

                            InboxViewTemplates.Add(_template);

                            if (!InboxViewTemplatesDictionary.ContainsKey(item.RequestId.ToString()))
                            {
                                InboxViewTemplatesDictionary.Add(item.RequestId.ToString(), _template);
                            }



                        }


                    }

                }
                catch (Exception ex)
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
