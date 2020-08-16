
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Lottie.Forms.Droid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QuickOrderApp.Droid
{
    [Activity(Label = "QuickOrderApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //AnimationViewRenderer.Init();
            //AnimationViewRenderer.Init();
            AnimationViewRenderer.Init();

            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();


            LoadApplication(Startup.Init(ConfigureServices));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            //services.AddSingleton<ISoftwareKeyboardService, SoftwareKeyboardService>();
            // services.AddSingleton<Activity,MainActivity>();
        }
    }
}