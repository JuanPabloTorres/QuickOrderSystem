using Library.DTO;
using Library.Models;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class SearchEmployeePresenter:BaseViewModel
    {
		private Guid userId;

		public Guid UserId
		{
			get { return userId; }
			set { userId = value;
				OnPropertyChanged();
			}
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value;
				OnPropertyChanged();
			}
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value;
				OnPropertyChanged();
			}
		}

		private string phone;

		public string Phone
		{
			get { return phone; }
			set { phone = value;
				OnPropertyChanged();
			}
		}

		private string gender;

		public string Gender
		{
			get { return gender; }
			set { gender = value;
				OnPropertyChanged();
			}
		}

		private Guid requesttostore;

		public Guid RequestToStore
		{
			get { return requesttostore; }
			set { requesttostore = value; }
		}



		public ICommand SendRequestCommand { get; set; }

		public SearchEmployeePresenter(UserDTO userDTO,string storeId)
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

                if (!isEmployee)
                {
                    var jobRequest = new UserRequest()
                    {
                        RequestId = Guid.NewGuid(),
                        FromStore = RequestToStore,
                        ToUser = UserId,
                        Type = RequestType.JobRequest,
                        RequestAnswer = Answer.None
                    };

                    var userhaveOrder = RequestDataStore.UserRequestExists(UserId, RequestToStore);

                    if (!userhaveOrder)
                    {

                        var result = await RequestDataStore.AddItemAsync(jobRequest);

                        var userhubconnectionResult = await userConnectedDataStore.GetUserConnectedID(jobRequest.ToUser);

                        await App.ComunicationService.SendRequestToUser(userhubconnectionResult.HubConnectionID, jobRequest);


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






	}
}
