using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HReception.UI;
using FreshMvvm;
using HReception.Logic.Context.Infrastructure;
using HReception.Droid.Services;

namespace HReception.Droid
{
    [Activity(Label = "HReception", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            PlatformDependencyRegistrar();
            LoadApplication(new App());
        }
        private void PlatformDependencyRegistrar()
        {
            FreshIOC.Container.Register<IDbHelper, DbHelper>();
        }
    }
}