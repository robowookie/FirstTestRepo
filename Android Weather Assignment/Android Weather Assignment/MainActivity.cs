using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Android_Weather_Assignment
{
    [Activity(Label = "Android_Weather_Assignment", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView lv_CityWeather;
        List<Current_observation> curList;
        RESTHandler objRest;
        TextView tv_Date;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //display today's date
            tv_Date = FindViewById<TextView>(Resource.Id.tv_Date);
            tv_Date.Text = DateTime.Today.Date.ToShortDateString();

            curList = new List<Current_observation>();

            //creating list of objects that come from weather api
            listCities("Auckland");
            listCities("Hamilton");
            listCities("Wellington");
            listCities("Christchurch");

            //using object list to populate listview
            lv_CityWeather = FindViewById<ListView>(Resource.Id.lv_CityWeather);
            lv_CityWeather.Adapter = new dataAdapterMain(this, curList);

            lv_CityWeather.ItemClick += Lv_CityWeather_ItemClick;

        }

        private void Lv_CityWeather_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //open forecast page for item selected
            Intent i = new Intent(this, typeof(ForecastActivity));
            i.PutExtra("city", curList[e.Position].Display_location.City);
            StartActivity(i);
        }

        public void listCities(string city)
        {
            objRest = new RESTHandler(@"http://api.wunderground.com/api/56336fc8b313eb0c/conditions/q/NZ/" + city + ".xml");

            var conditions = objRest.ExecuteRequest();
            curList.Add(conditions.Current_observation);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Get Current Location's Weather");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();

            switch (itemTitle)
            {
                case "Get Current Location's Weather":
                    Intent i = new Intent(this, typeof(CurrentLocationActivity));
                    StartActivity(i);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

