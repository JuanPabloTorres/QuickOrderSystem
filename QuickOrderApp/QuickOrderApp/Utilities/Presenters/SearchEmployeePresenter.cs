using Library.DTO;
using Library.Helpers;
using Library.Models;
using QuickOrderApp.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class SearchEmployeePresenter : BaseViewModel
    {
        private string email;

        private string gender;

        private string name;

        private string phone;

        private Guid requesttostore;

        private Guid userId;

        public SearchEmployeePresenter (UserDTO userDTO, string storeId)
        {
            this.UserId = userDTO.UserId;

            this.Name = userDTO.Name;

            this.Phone = userDTO.Phone;

            this.Gender = userDTO.Gender.ToString();

            this.Email = userDTO.Phone;

            this.RequestToStore = Guid.Parse(storeId);

            SendRequestCommand = new Command(async () =>
            {
                bool isEmployee = await EmployeeDataStore.IsEmployeeFromStore(RequestToStore, UserId);

                if( !isEmployee )
                {
                    var jobRequest = new UserRequest()
                    {
                        ID = Guid.NewGuid(),
                        FromStore = RequestToStore,
                        ToUser = UserId,
                        Type = RequestType.JobRequest,
                        RequestAnswer = Answer.None
                    };

                    var userhaveOrder = RequestDataStore.UserRequestExists(UserId, RequestToStore);

                    if( !userhaveOrder )
                    {
                        var result = await RequestDataStore.AddItemAsync(jobRequest);

                        var userhubconnectionResult = await userConnectedDataStore.GetUserConnectedID(jobRequest.ToUser);

                        if( userhubconnectionResult != null )
                        {
                            await App.ComunicationService.SendRequestToUser(userhubconnectionResult.HubConnectionID, jobRequest);
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Notification", "Request was sended is waiting for response.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Is Employee", "OK");
                }
            });
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;

                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;

                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;

                OnPropertyChanged();
            }
        }

        public Guid RequestToStore
        {
            get { return requesttostore; }
            set { requesttostore = value; }
        }

        public ICommand SendRequestCommand { get; set; }

        public Guid UserId
        {
            get { return userId; }
            set
            {
                userId = value;

                OnPropertyChanged();
            }
        }
    }
}