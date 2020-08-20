using Library.DTO;
using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
	[QueryProperty("StoreId","id")]
    public class SearchEmployeeViewModel:BaseViewModel
    {

		private string storeId;

		public string StoreId
		{
			get { return storeId; }
			set { storeId = value;
				OnPropertyChanged();
			}
		}


		public ObservableCollection<SearchEmployeePresenter> Users { get; set; }

		private string tosearch;

		public string ToSearch
		{
			get { return tosearch; }
			set { tosearch = value;
				OnPropertyChanged();
			}
		}


		public ICommand SearchEmployeeCommand { get; set; }

        public SearchEmployeeViewModel()
        {
            Users = new ObservableCollection<SearchEmployeePresenter>();
            SearchEmployeeCommand = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(ToSearch))
                {
                    await SearchEmployee(ToSearch);

                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "User with that information was not found...!", "OK");
                }


            });

        }


        async Task SearchEmployee(string value)
		{
			var usersdto = await userDataStore.GetUserWithName(value);

			foreach (var item in usersdto)
			{

				var serchEmpPresenter = new SearchEmployeePresenter(item,StoreId);
				Users.Add(serchEmpPresenter);
			}

			
		}

	}
}
