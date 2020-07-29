﻿using Library.Models;
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
        public ObservableCollection<RequestPresenter> UserRequests { get; set; }

        public InboxViewModel()
        {
            UserRequests = new ObservableCollection<RequestPresenter>();

            MessagingCenter.Subscribe<RequestPresenter>(this, "RefreshInbox", (sender) =>
            {

                UserRequests.Remove(sender);
                //LoadItemsCommand.Execute(null);
            });
        }


        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                UserRequests.Clear();
                //
                var requestData = await requestDataStore.GetRequestOfUser(App.LogUser.UserId);
                foreach (var item in requestData)
                {
                    if (item.RequestAnswer == Answer.None)
                    {
                        var presenter = new RequestPresenter(item);

                        UserRequests.Add(presenter);

                    }
                }
                //UserOrders = new ObservableCollection<Order>(userOrderData);
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
