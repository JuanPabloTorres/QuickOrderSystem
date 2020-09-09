using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
	[QueryProperty("OrderScannedId", "Id")]
   public  class ScannerViewModel:BaseViewModel
    {

		private string orderIdScanned;

		public string OrderIdScanned
		{
			get { return orderIdScanned; }
			set { orderIdScanned = value;
				OnPropertyChanged();
			}
		}

		private string orderscannedId;

		public string OrderScannedId
		{
			get { return orderscannedId; }
			set { orderscannedId = value;
				OnPropertyChanged();

				orderDataStore.GetItemAsync(OrderScannedId);
			}
		}


		private Order orderScanned;

		public Order OrderScanned
		{
			get { return orderScanned; }
			set { orderScanned = value;
				OnPropertyChanged();
			}
		}



		public ICommand ScanndCommad => new Command (() => 
		{
			

		});
		public ScannerViewModel()
		{

			MessagingCenter.Subscribe<Order>(this, "orderscanned", (sender) => 
			{

				OrderScanned = sender;
			
			});

		}

	

	}
}
