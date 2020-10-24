﻿using QuickOrderApp.ViewModels.LoginVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public static string Route = "RegisterPageRoute";
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}