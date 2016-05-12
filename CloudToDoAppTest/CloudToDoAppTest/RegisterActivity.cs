using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinAndroid;

namespace CloudToDoAppTest
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        string appKey = "kid_byQC7iXPMb";
        string appSecret = "5ff51b6d3f5e4ec2bf44f25418f1283b";
        Client kinveyClient;
        EditText et_username, et_password;
        Button btn_register;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            et_username = FindViewById<EditText>(Resource.Id.et_username);
            et_password = FindViewById<EditText>(Resource.Id.et_password);
            btn_register = FindViewById<Button>(Resource.Id.btn_register);

            kinveyClient = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
            .setOfflinePlatform(new SQLitePlatformAndroid())
            .build();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }

            btn_register.Click += Btn_register_Click;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }
        }

        private void Btn_register_Click(object sender, EventArgs e)
        {
            RegisterNewUser();
        }

        private async void RegisterNewUser ()
        {
            if (et_username.Text != "" && et_password.Text!= "")
            {
                try
                {
                    User myUser = await kinveyClient.User().CreateAsync(et_username.Text, et_password.Text);
                    StartActivity(typeof(MainActivity));
                    Toast.MakeText(this, "New user '" + et_username.Text + "' has been created.", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, "Error: " + ex, ToastLength.Short).Show();
                }
            }
        }
    }
}