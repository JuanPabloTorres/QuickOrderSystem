using Library.DTO;
using QuickOrderApp.ViewModels;
using QuickOrderApp.Views.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
	public class PaymentCardPresenter : BaseViewModel
	{
		private string holdername;

		public string HolderName
		{
			get { return holdername; }
			set { holdername = value;
				OnPropertyChanged();
			}
		}

		private string stripecardId;

		public string StipeCardId
		{
			get { return stripecardId; }
			set { stripecardId = value;
				OnPropertyChanged();
			}
		}



		private Guid paymentCardId;

		public Guid PaymentCardId
		{
			get { return paymentCardId; }
			set { paymentCardId = value;
				OnPropertyChanged();
			}
		}

		private string cardnumber;

		public string CardNumber
		{
			get { return cardnumber; }
			set { cardnumber = value;
				OnPropertyChanged();
			}
		}

		private string expmonth;

		public string ExpMonth
		{
			get { return expmonth; }
			set { expmonth = value;
				OnPropertyChanged();
			}
		}

		private string expyear;

		public string ExpYear
		{
			get { return expyear; }
			set { expyear = value;
				OnPropertyChanged();
			}
		}

		private string cvc;

		public string CVC
		{
			get { return cvc; }
			set { cvc = value;
				OnPropertyChanged();
			}
		}

		public ICommand DeleteCardCommand { get; set; }


		public PaymentCardPresenter(PaymentCardDTO paymentCardDTO)
		{
			this.HolderName = paymentCardDTO.HolderName;
			this.CardNumber = paymentCardDTO.CardNumber;
			this.stripecardId = paymentCardDTO.StripeCardId;
			this.PaymentCardId = paymentCardDTO.PaymentCardId;


			DeleteCardCommand = new Command(async() => 
			{


				var deleted = await CardDataStore.DeletePaymentCard(paymentCardId.ToString());

				if (deleted)
				{
					var cardDeleteStripeResult = await stripeServiceDS.DeleteCardFromCustomer(App.LogUser.StripeUserId, this.StipeCardId);

					 if (cardDeleteStripeResult)
					{
						MessagingCenter.Send<PaymentCardPresenter>(this, "PaymencardDeleteMsg"); 
					}
				}


				//await Shell.Current.GoToAsync($"{EditCardPage.Route}");
			  
			});


		}








	}
}
