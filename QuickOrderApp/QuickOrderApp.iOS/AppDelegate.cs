using FFImageLoading.Forms.Platform;
using Foundation;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.ComboBox;
using UIKit;

namespace QuickOrderApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            CachedImageRenderer.Init();
            SfPickerRenderer.Init();
            SfComboBoxRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfButtonRenderer.Init();
            SfNumericUpDownRenderer.Init();

            LoadApplication(Startup.Init(ConfigureServices));

            return base.FinishedLaunching(app, options);
        }

        private void ConfigureServices(Microsoft.Extensions.Hosting.HostBuilderContext ctx, Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
        }
    }
}
