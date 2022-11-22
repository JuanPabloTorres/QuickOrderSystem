using Library.Models;
using QuickOrderApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class RequestPresenter : BaseViewModel
    {
        private Guid requestid;

        private string store;

        private string title;

        private Guid toUser;

        public RequestPresenter (UserRequest userRequest)
        {
            RequestId = userRequest.RequestId;

            var store = GetStore(userRequest.FromStore.ToString());

            if( userRequest.Type == RequestType.JobRequest )
            {
                RequestTitle = store.Result.StoreName + " Send Job Request";
            }

            RequestAnswerCommand = new Command<string>(async (e) =>
            {
                if( Answer.Accept.ToString() == e )
                {
                    userRequest.RequestAnswer = Answer.Accept;

                    var requestUpdated = await requestDataStore.UpdateItemAsync(userRequest);

                    if( requestUpdated )
                    {
                        MessagingCenter.Send<RequestPresenter>(this, "RefreshInbox");

                        var newStoreEmployee = new Employee()
                        {
                            EmployeeId = Guid.NewGuid(),
                            UserId = userRequest.ToUser,
                            StoreId = userRequest.FromStore,
                            Type = EmployeeType.New
                        };

                        var employeeAdded = await EmployeeDataStore.AddItemAsync(newStoreEmployee);
                    }
                }
                if( Answer.Decline.ToString() == e )
                {
                    //userRequest.RequestAnswer = Answer.Decline;
                    //var requestUpdated = await requestDataStore.UpdateItemAsync(userRequest);
                    //MessagingCenter.Send<RequestPresenter>(this, "RefreshInbox");
                    var deleted = await requestDataStore.DeleteItemAsync(RequestId.ToString());

                    MessagingCenter.Send<RequestPresenter>(this, "RefreshInbox");
                }
            });
        }

        public ICommand RequestAnswerCommand { get; set; }

        public Guid RequestId
        {
            get { return requestid; }
            set
            {
                requestid = value;

                OnPropertyChanged();
            }
        }

        public string RequestTitle
        {
            get { return title; }
            set { title = value; }
        }

        public string Store
        {
            get { return store; }
            set
            {
                store = value;

                OnPropertyChanged();
            }
        }

        public Guid ToUser
        {
            get { return toUser; }
            set
            {
                toUser = value;
                OnPropertyChanged();
            }
        }

        private async Task<Store> GetStore (string storeId)
        {
            var store = await StoreDataStore.GetItemAsync(storeId);

            return store;
        }
    }
}