using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Database;
using Walkies.Core;
using System;

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
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += OnLoginClick;
        }

        async void OnLoginClick(object sender, EventArgs e)
        {
            string username = FindViewById<EditText>(Resource.Id.editText1).Text;
            string password = FindViewById<EditText>(Resource.Id.editText2).Text;
            bool login = DataBaseGrabber.LoginTry(username, password);
            if (login == true)
            {
                SetContentView(Resource.Layout.Search);
            }
            else
                FindViewById<TextView>(Resource.Id.textView1).Text = "Incorrect Username or Password";
        }
    }
}

