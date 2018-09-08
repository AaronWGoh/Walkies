using Android.App;
using Android.Widget;
using Android.OS;

namespace Walkies.Android
{
    [Activity(Label = "Walkies.Android", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
           // Button button_Home = FindViewById<Button>(Resource.Id.newButton);

        

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

