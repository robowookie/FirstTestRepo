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
using RestSharp;
using System.Xml.Serialization;
using System.IO;

namespace Android_Weather_Assignment
{
    public class RESTHandler
    {
        private string url;
        private IRestResponse response;

        public RESTHandler()
        {
            url = "";
        }

        public RESTHandler(string lurl)
        {
            url = lurl;
        }

        public Conditions ExecuteRequest()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = client.Execute(request);

            XmlSerializer serializer = new XmlSerializer(typeof(Conditions));

            Conditions objRes;

            StringReader sr = new StringReader(response.Content);

            objRes = serializer.Deserialize(sr) as Conditions;

            return objRes;
        }

        public Forecasts ExecuteRequestForecast()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = client.Execute(request);

            XmlSerializer serializer = new XmlSerializer(typeof(Forecasts));

            Forecasts objRes;

            StringReader sr = new StringReader(response.Content);

            objRes = serializer.Deserialize(sr) as Forecasts;

            return objRes;
        }

        public LocationGeoLookUp ExecuteRequestGeoLookUp()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = client.Execute(request);

            XmlSerializer serializer = new XmlSerializer(typeof(LocationGeoLookUp));

            LocationGeoLookUp objRes;

            StringReader sr = new StringReader(response.Content);

            objRes = serializer.Deserialize(sr) as LocationGeoLookUp;

            return objRes;
        }
    }
}