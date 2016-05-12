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
using Android.Graphics.Drawables;

namespace Android_Weather_Assignment
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        ListView lv_Forecast;
        List<Forecastday> forList;
        RESTHandler objRest;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Forecast);
            // Create your application here

            lv_Forecast = FindViewById<ListView>(Resource.Id.lv_Forecast);

            forList = new List<Forecastday>();

            //load city API dependent on the city selected in the previous activity
            listForecast(Intent.GetStringExtra("city"));

            lv_Forecast.Adapter = new dataAdapterForecast(this, forList);

        }

        public void listForecast(string city)
        {
            objRest = new RESTHandler(@"http://api.wunderground.com/api/56336fc8b313eb0c/forecast10day/q/NZ/" + city + ".xml");

            var forecasts = objRest.ExecuteRequestForecast();
            //use the forecastday list from API to create new list
            foreach(Forecastday f in forecasts.Forecast.Simpleforecast.Forecastdays.Forecastday)
            {
                forList.Add(f);
            }
            
        }
    }
}