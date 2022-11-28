using Library.Helpers;
using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
         StoreViewModel _StoreViewModel;

        public Products()
        {
            InitializeComponent();

            BindingContext = _StoreViewModel = new StoreViewModel();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _StoreViewModel.StoreProducts.Clear();

            ProductType _productType = (ProductType)Enum.Parse(typeof(ProductType), _StoreViewModel.SelectedProductType);

            //Guid guidStoreId = Guid.Parse(StoreId);

            var data = await _StoreViewModel.productDataStore.GetSpecificProductTypeFromStore(App.CurrentStore.StoreID, _productType);

            foreach (var item in data)
            {

                var productPresenter = new ProductPresenter(item);
                _StoreViewModel.StoreProducts.Add(productPresenter);
            }

        }


    }
}
