using Library.Models;
using QuickOrderApp.Utilities.Templates;
using QuickOrderApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class MessageInboxPresenter : BaseViewModel
    {
        private string store;

        public string Store
        {
            get { return store; }
            set
            {
                store = value;
                OnPropertyChanged();
            }
        }

        private string title;

        public string RequestTitle
        {
            get { return title; }
            set { title = value; }
        }

        private Guid requestid;

        public Guid RequestId
        {
            get { return requestid; }
            set
            {
                requestid = value;
                OnPropertyChanged();
            }
        }

        private Guid toUser;

        public Guid ToUser
        {
            get { return toUser; }
            set {
                toUser = value;
                OnPropertyChanged();
            }
        }


       

        private string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value;
                OnPropertyChanged();
            }
        }


        public ICommand CopyMsgCommand => new Command(async() => 
        {


            await Clipboard.SetTextAsync(Msg);

            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();

                await Shell.Current.DisplayAlert("Success", string.Format("Your copied text is({0})", text), "OK");
            }

        });
        public ICommand RequestAnswerCommand { get; set; }

        public ICommand MarkAsReadCommand { get; set; }
        public MessageInboxPresenter(UserRequest userRequest)
        {
            RequestId = userRequest.RequestId;

            var store = GetStore(userRequest.FromStore.ToString());

            if (userRequest.Type == RequestType.JobRequest)
            {
                RequestTitle = store.Result.StoreName + " Send Job Request";
                
            }
            if (userRequest.Type == RequestType.StoreLicensesRequest)
            {
                RequestTitle = "Store License";
                Msg = userRequest.Message;
               
            }

            MarkAsReadCommand = new Command(async()=>
            {

                userRequest.RequestAnswer = Answer.Read;

                await requestDataStore.UpdateItemAsync(userRequest);

                MessagingCenter.Send<MessageInboxPresenter>(this, "RefreshInbox");

            });

            RequestAnswerCommand = new Command<string>(async (e) =>
            {

                if (Answer.Accept.ToString() == e)
                {

                    userRequest.RequestAnswer = Answer.Accept;

                    var requestUpdated = await requestDataStore.UpdateItemAsync(userRequest);

                    if (requestUpdated)
                    {

                        MessagingCenter.Send<MessageInboxPresenter>(this, "RefreshInbox");

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
                if (Answer.Decline.ToString() == e)
                {
                    
                    var deleted = await requestDataStore.DeleteItemAsync(RequestId.ToString());

                    MessagingCenter.Send<MessageInboxPresenter>(this, "RefreshInbox");
                }

            });
        }

        async Task<Store> GetStore(string storeId)
        {
            var store = await StoreDataStore.GetItemAsync(storeId);

            return store;
        }
    }
}
