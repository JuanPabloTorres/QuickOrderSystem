using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickOrderApp.ViewModels.SettingVM;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStorePage : ContentPage
    {
        public RegisterStorePage()
        {
            InitializeComponent();
            BindingContext = new RegisterStoreViewModel();
        }

       
    }
}