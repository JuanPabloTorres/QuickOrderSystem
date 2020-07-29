using Library.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "Id")]
    public class StoreHomeViewModel : BaseViewModel
    {

        private string storeid;

        public string StoreId
        {
            get { return storeid; }
            set
            {
                storeid = value;
                OnPropertyChanged();

                GetStore(StoreId);


            }
        }

        public ObservableCollection<Product> StoreProducts { get; set; }

        private string storename;

        public string StoreName
        {
            get { return storename; }
            set
            {
                storename = value;
                OnPropertyChanged();
            }
        }


        public StoreHomeViewModel()
        {

        }

        async Task GetStore(string id)
        {
            var store = await StoreDataStore.GetItemAsync(StoreId);

            StoreName = store.StoreName;

        }

    }
}
