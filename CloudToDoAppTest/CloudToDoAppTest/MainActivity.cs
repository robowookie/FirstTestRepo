using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinAndroid;

namespace CloudToDoAppTest
{
    [Activity(Label = "CloudToDoAppTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string appKey = "kid_byQC7iXPMb";
        string appSecret = "5ff51b6d3f5e4ec2bf44f25418f1283b";
        Client kinveyClient;
        EditText et_username, et_password;
        Button btn_login, btn_goToRegister;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btn_goToRegister = FindViewById<Button>(Resource.Id.btn_goToRegister);
            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            et_username = FindViewById<EditText>(Resource.Id.et_username);
            et_password = FindViewById<EditText>(Resource.Id.et_password);

            kinveyClient = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
            .setOfflinePlatform(new SQLitePlatformAndroid())
            .build();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }

            btn_goToRegister.Click += Btn_goToRegister_Click;
            btn_login.Click += Btn_login_Click;

        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            loginUser();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //if (kinveyClient.User().isUserLoggedIn())
            //{
            //    kinveyClient.User().Logout();
            //}
        }

        private async void loginUser()
        {
            if (et_username.Text != "" && et_password.Text != "")
            {
                try
                {
                    User user = await kinveyClient.User().LoginAsync(et_username.Text, et_password.Text);
                    Toast.MakeText(this, "You have logged in.", ToastLength.Short).Show();
                    StartActivity(typeof(ListActivity));
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Error: " + ex, ToastLength.Short).Show();
                }
            }
            
            
        }

        private void Btn_goToRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }
    }
}

