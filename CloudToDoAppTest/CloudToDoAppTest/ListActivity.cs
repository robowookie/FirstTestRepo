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
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        string appKey = "kid_byQC7iXPMb";
        string appSecret = "5ff51b6d3f5e4ec2bf44f25418f1283b";
        Client kinveyClient;
        Button btn_addNote, btn_editNote, btn_deleteNote;
        EditText et_titleNew, et_noteNew;
        string myUser, editID;
        ListView listView1;
        List<ToDoClass> templist;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.List);

            btn_addNote = FindViewById<Button>(Resource.Id.btn_addNote);
            btn_editNote = FindViewById<Button>(Resource.Id.btn_editNote);
            btn_deleteNote = FindViewById<Button>(Resource.Id.btn_deleteNote);
            et_titleNew = FindViewById<EditText>(Resource.Id.et_titleNew);
            et_noteNew = FindViewById<EditText>(Resource.Id.et_noteNew);
            listView1 = FindViewById<ListView>(Resource.Id.listView1);


            kinveyClient = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
            .setOfflinePlatform(new SQLitePlatformAndroid())
            .build();

            myUser = kinveyClient.ClientUsers.CurrentUser;

            //if (kinveyClient.User().isUserLoggedIn())
            //{
            //    kinveyClient.User().Logout();
            //}

            getAllItems();

            btn_addNote.Click += Btn_addNote_Click;
            btn_editNote.Click += Btn_editNote_Click;
            btn_deleteNote.Click += Btn_deleteNote_Click;
            listView1.ItemClick += ListView1_ItemClick;
        }

        private void Btn_deleteNote_Click(object sender, EventArgs e)
        {
            deleteItem();
        }

        private void Btn_editNote_Click(object sender, EventArgs e)
        {
            editItem();
        }

        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var myList = templist[e.Position];
            et_titleNew.Text = myList.title;
            et_noteNew.Text = myList.note;
            editID = myList.id;

        }

        private void Btn_addNote_Click(object sender, EventArgs e)
        {
            addItem();
        }

        public async void addItem()
        {
            ToDoClass item = new ToDoClass();

            item.title = et_titleNew.Text;
            item.note = et_noteNew.Text;
            item.user = myUser;

            AsyncAppData<ToDoClass> myBook = kinveyClient.AppData<ToDoClass>("tblOfWhatevs", typeof(ToDoClass));
            ToDoClass saved = await myBook.SaveAsync(item);
            getAllItems();
        }

        public async void editItem()
        {
            AsyncAppData<ToDoClass> myBook = kinveyClient.AppData<ToDoClass>("tblOfWhatevs", typeof(ToDoClass));
            ToDoClass editList = await myBook.GetEntityAsync(editID);

            editList.title = et_titleNew.Text;
            editList.note = et_noteNew.Text;

            ToDoClass saved = await myBook.SaveAsync(editList);
            getAllItems();
        }

        public async void deleteItem()
        {
            if (editID!="")
            {
                AsyncAppData<ToDoClass> myBook = kinveyClient.AppData<ToDoClass>("tblOfWhatevs", typeof(ToDoClass));
                await myBook.DeleteAsync(editID);
                getAllItems();
                Toast.MakeText(this, "Entry Deleted.", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "No Entry Selected.", ToastLength.Short).Show();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }
        }

        public async void getAllItems()
        {
            AsyncAppData<ToDoClass> item = kinveyClient.AppData<ToDoClass>("tblOfWhatevs", typeof(ToDoClass));
            ToDoClass[] itemList = await item.GetAsync();

            templist = itemList.Where(p => p.user == myUser).ToList();
            //List<ToDoClass> templist = itemList.ToList();
            listView1.Adapter = new dataAdapter(this, templist);
            Toast.MakeText(this, "Items Get!", ToastLength.Short).Show();
            et_titleNew.Text = "";
            et_noteNew.Text = "";
            editID = "";
        }
    }
}