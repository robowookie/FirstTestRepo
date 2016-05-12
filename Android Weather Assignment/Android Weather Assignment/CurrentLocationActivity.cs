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
using Android.Locations;
using Android.Util;
using System.Threading.Tasks;

namespace Android_Weather_Assignment
{
    [Activity(Label = "CurrentLocationActivity")]
    public class CurrentLocationActivity : Activity, ILocationListener
    {
        LocationManager _locationManager;
        string _locationProvider, _lat, _long;
        RESTHandler objRest;
        TextView tv_CurrentStation, tv_Temp, tv_HumidityData;
        ImageView iv_Image;

        public void OnLocationChanged(Location location)
        {
            //_currentLocation = location;

            //whenever location is changed, assign new latitude and longitude
            _lat = location.Latitude.ToString();
            _long = location.Longitude.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        protected override void OnResume()
        {
            base.OnResume();
            //_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

            //check for new location every time app resumes
            if (_locationManager.AllProviders.Contains(LocationManager.NetworkProvider)
                && _locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 2000, 1, this);
                Toast.MakeText(this, "wefuihwefuhwefuwefuhwefiuhe", ToastLength.Short).Show();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            //_locationManager.RemoveUpdates(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CurrentLocation);
            // Create your application here

            tv_CurrentStation = FindViewById<TextView>(Resource.Id.tv_CurrentStation);
            tv_Temp = FindViewById<TextView>(Resource.Id.tv_LowTemp);
            tv_HumidityData = FindViewById<TextView>(Resource.Id.tv_HumidityData);
            iv_Image = FindViewById<ImageView>(Resource.Id.iv_Image);

            _locationManager = GetSystemService(Context.LocationService) as LocationManager;
            //InitializeLocationManager();

            //check for location on activity startup
            if(_locationManager.AllProviders.Contains(LocationManager.NetworkProvider)
                && _locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 2000, 1, this);
                Toast.MakeText(this, "wefuihwefuhwefuwefuhwefiuhe", ToastLength.Short).Show();
            }

            //if (_currentLocation == null)
            //{
            //    Toast.MakeText(this, "Can't determine the current address. Try again in a few minutes.", ToastLength.Long).Show();
            //    return;
            //}

            //address = ReverseGeocodeCurrentLocation();
            
            //geocode API will return location information
            var Locat = getLocation("-36.848461", "174.763336");

            //'first' will indicate closest weather station to co-ordinates
            var first = Locat.Nearby_weather_stations.Airport.Station[0];

            //get the weather based on the closest weather station
            Conditions weather = getWeather(first.Country, first.City);

            //display location, temp info and weather image similar to first screen
            tv_CurrentStation.Text = weather.Current_observation.Display_location.Full;
            tv_Temp.Text = weather.Current_observation.Temp_c + "°C";
            tv_HumidityData.Text = weather.Current_observation.Relative_humidity;
        }

        public Conditions getWeather(string country, string city)
        {
            //RESTHandler to get weather API info based on country and city name
            objRest = new RESTHandler(@"http://api.wunderground.com/api/56336fc8b313eb0c/conditions/q/" + country + "/" + city + ".xml");

            Conditions gotWeather = objRest.ExecuteRequest();

            return gotWeather;
        }

        public LocationGeoLookUp getLocation(string lat, string lon)
        {
            //RESTHandler to get geocode API info based on latitude and longitude name
            //objRest = new RESTHandler(@"http://api.wunderground.com/api/56336fc8b313eb0c/geolookup/q/" + lat + "," + lon + ".xml");
            objRest = new RESTHandler(@"http://api.wunderground.com/api/56336fc8b313eb0c/geolookup/q/-36.848461,174.763336.xml");

            var gotLocat = objRest.ExecuteRequestGeoLookUp();

            return gotLocat;
        }

        #region:unecessary test code

        //public Address ReverseGeocodeCurrentLocation()
        //{
        //    Geocoder geocoder = new Geocoder(this);
        //    IList<Address> addressList =
        //        geocoder.GetFromLocation(_currentLocation.Latitude, _currentLocation.Longitude, 10);

        //    Address address = addressList.FirstOrDefault();
        //    return address;
        //}

        //public void InitializeLocationManager()
        //{
        //    _locationManager = (LocationManager)GetSystemService(LocationService);
        //    Criteria criteriaForLocationService = new Criteria
        //    {
        //        Accuracy = Accuracy.Fine
        //    };
        //    IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

        //    if (acceptableLocationProviders.Any())
        //    {
        //        _locationProvider = acceptableLocationProviders.First();
        //    }
        //    else
        //    {
        //        _locationProvider = string.Empty;
        //    }
        //}
        #endregion
    }
}