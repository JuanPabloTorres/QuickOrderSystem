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
            BindingContext = new SettingViewModel();
        }

        private async void pickphoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,

            });


            if (file == null)
                return;

            //files.Add(file);

            logo.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });


            //storeImage.Source = ImageSource.FromFile(file.Path);


        }
    }
}