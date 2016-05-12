/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Android_Weather_Assignment
{
    [XmlRoot(ElementName = "features")]
    public class FeaturesGeoLookUp
    {
        [XmlElement(ElementName = "feature")]
        public string Feature { get; set; }
    }

    [XmlRoot(ElementName = "station")]
    public class Station
    {
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "icao")]
        public string Icao { get; set; }
        [XmlElement(ElementName = "lat")]
        public string Lat { get; set; }
        [XmlElement(ElementName = "lon")]
        public string Lon { get; set; }
        [XmlElement(ElementName = "neighborhood")]
        public string Neighborhood { get; set; }
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "distance_km")]
        public string Distance_km { get; set; }
        [XmlElement(ElementName = "distance_mi")]
        public string Distance_mi { get; set; }
    }

    [XmlRoot(ElementName = "airport")]
    public class Airport
    {
        [XmlElement(ElementName = "station")]
        public List<Station> Station { get; set; }
    }

    [XmlRoot(ElementName = "pws")]
    public class Pws
    {
        [XmlElement(ElementName = "station")]
        public List<Station> Station { get; set; }
    }

    [XmlRoot(ElementName = "nearby_weather_stations")]
    public class Nearby_weather_stations
    {
        [XmlElement(ElementName = "airport")]
        public Airport Airport { get; set; }
        [XmlElement(ElementName = "pws")]
        public Pws Pws { get; set; }
    }

    [XmlRoot(ElementName = "location")]
    public class LocationGeoLookUp
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "country_iso3166")]
        public string Country_iso3166 { get; set; }
        [XmlElement(ElementName = "country_name")]
        public string Country_name { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "tz_short")]
        public string Tz_short { get; set; }
        [XmlElement(ElementName = "tz_long")]
        public string Tz_long { get; set; }
        [XmlElement(ElementName = "lat")]
        public string Lat { get; set; }
        [XmlElement(ElementName = "lon")]
        public string Lon { get; set; }
        [XmlElement(ElementName = "zip")]
        public string Zip { get; set; }
        [XmlElement(ElementName = "magic")]
        public string Magic { get; set; }
        [XmlElement(ElementName = "wmo")]
        public string Wmo { get; set; }
        [XmlElement(ElementName = "l")]
        public string L { get; set; }
        [XmlElement(ElementName = "requesturl")]
        public string Requesturl { get; set; }
        [XmlElement(ElementName = "wuiurl")]
        public string Wuiurl { get; set; }
        [XmlElement(ElementName = "nearby_weather_stations")]
        public Nearby_weather_stations Nearby_weather_stations { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class ResponseGeoLookUp
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "termsofService")]
        public string TermsofService { get; set; }
        [XmlElement(ElementName = "features")]
        public Features Features { get; set; }
        [XmlElement(ElementName = "location")]
        public LocationGeoLookUp Location { get; set; }
    }

}
