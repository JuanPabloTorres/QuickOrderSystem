using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class ProductCategoryPresenter
    {

        public string Category { get; set; }

        public ICommand GoInventoryCommand { get; set; }

        public ProductCategoryPresenter(string category)
        {
            this.Category = category;

            GoInventoryCommand = new Command<string>(async (arg) => 
            {

                await Shell.Current.GoToAsync($"ProductRoute?type={arg}");
            
            });
        }

    }
}
