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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Android.Content.Res;

namespace Android_Weather_Assignment
{

    public class dataAdapterForecast : BaseAdapter<Forecastday>
    {
        int[] drawables;
        string[] conditions;
        List<Forecastday> items;

        Activity context;
        public dataAdapterForecast(Activity context, List<Forecastday> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Forecastday this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ForecastItemLayout, null);

            view.FindViewById<TextView>(Resource.Id.tv_Day).Text = item.Date.Weekday_short + ", " + item.Date.Monthname_short + " " + item.Date.Day;
            view.FindViewById<TextView>(Resource.Id.tv_LowTemp).Text = item.Low.Celsius + "°C";
            view.FindViewById<TextView>(Resource.Id.tv_HighTemp).Text = item.High.Celsius + "°C";

            if (item.Icon != null)
            {
                assignImages();

                //where conditions index matches drawables index (from assignImages() generated arrays,
                //load the matching drawable file to the image views
                int temp = Array.IndexOf(conditions, conditions.Where(x => x.Equals(item.Icon)).SingleOrDefault());

                view.FindViewById<ImageView>(Resource.Id.iv_Image).SetImageResource(drawables[temp]);
            }

            return view;
        }

        public void assignImages()
        {
            drawables = new int[] { Resource.Drawable.chanceflurries, Resource.Drawable.chancerain,
                Resource.Drawable.chancesleet, Resource.Drawable.chancesnow, Resource.Drawable.chancetstorms,
            Resource.Drawable.clear, Resource.Drawable.cloudy, Resource.Drawable.flurries,
                Resource.Drawable.fog, Resource.Drawable.hazy, Resource.Drawable.mostlycloudy,
                Resource.Drawable.mostlysunny, Resource.Drawable.nt_chanceflurries,
                Resource.Drawable.nt_chancerain, Resource.Drawable.nt_chancesleet,
                Resource.Drawable.nt_chancesnow, Resource.Drawable.nt_chancetstorms,
                Resource.Drawable.nt_clear, Resource.Drawable.nt_cloudy, Resource.Drawable.nt_flurries,
                Resource.Drawable.nt_fog, Resource.Drawable.nt_hazy, Resource.Drawable.nt_mostlycloudy,
                Resource.Drawable.nt_mostlysunny, Resource.Drawable.nt_partlycloudy,
                Resource.Drawable.nt_partlysunny, Resource.Drawable.nt_rain, Resource.Drawable.nt_sleet,
                Resource.Drawable.nt_snow, Resource.Drawable.nt_sunny, Resource.Drawable.nt_tstorms,
                Resource.Drawable.partlycloudy, Resource.Drawable.partlysunny, Resource.Drawable.rain,
                Resource.Drawable.sleet, Resource.Drawable.snow, Resource.Drawable.sunny,
                Resource.Drawable.tstorms};

            conditions = new string[] { "chanceflurries", "chancerain", "chancesleet", "chancesnow", "chancetstorms",
                "clear", "cloudy", "flurries", "fog", "hazy", "mostlycloudy", "mostlysunny", "nt_chanceflurries",
                "nt_chancerain", "nt_chancesleet", "nt_chancesnow", "nt_chancetstorms", "nt_clear", "nt_cloudy",
                "nt_flurries", "nt_fog", "nt_hazy", "nt_mostlycloudy", "nt_mostlysunny", "nt_partlycloudy",
                "nt_partlysunny", "nt_rain", "nt_sleet", "nt_snow", "nt_sunny", "nt_tstorms", "partlycloudy",
                "partlysunny", "rain", "sleet", "snow", "sunny", "tstorms" };
        }

    }
}
