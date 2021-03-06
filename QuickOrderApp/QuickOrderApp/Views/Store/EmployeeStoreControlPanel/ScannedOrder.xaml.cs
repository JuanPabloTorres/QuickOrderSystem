﻿using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.EmployeeStoreControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedOrder : ContentPage
    {
        public static string Route = "ScannedOrder";
        public ScannedOrder()
        {
            InitializeComponent();
            BindingContext = new ScannerViewModel();
        }
    }
}