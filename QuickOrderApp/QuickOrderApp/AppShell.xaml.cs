using QuickOrderApp.Views.Home;
using QuickOrderApp.Views.Store;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace QuickOrderApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("HomePageRoute", typeof(HomePage));
            Routing.RegisterRoute("StoreHomeRoute", typeof(StoreHome));
            Routing.RegisterRoute("ProductRoute", typeof(Products));
            Routing.RegisterRoute("DetailOrderRoute", typeof(DetailOrder));
            Routing.RegisterRoute("StoreOrderRoute", typeof(StoreOrders));
        }
    }
}
