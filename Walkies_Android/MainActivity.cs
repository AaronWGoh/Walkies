using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Database;

namespace Walkies_Android
{
    [Activity(Label = "Walkies", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //OpenOrCreateDatabase();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

