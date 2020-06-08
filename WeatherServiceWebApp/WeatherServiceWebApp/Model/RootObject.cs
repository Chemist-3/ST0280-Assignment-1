using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherServiceWebApp.Model
{
    public class RootObject
    {
        public Items[] items { get; set; }
        public API_Info api_info { get; set; }
    }

    public class Items
    {
        public string update_timestamp { get; set; }
        public string timestamp { get; set; }
        public ValidPeriod valid_period { get; set; }
        public General general { get; set; }
        public Periods[] periods { get; set; }

    }
    public class ValidPeriod
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class General
    {
        public string forecast { get; set; }
        public RelativeHumidity relative_humidity { get; set; }
        public Temperature temperature { get; set; }
        public Wind wind { get; set; }
    }
    public class Periods
    {
        public Time time { get; set; }
        public Regions regions { get; set; }
    }

    public class RelativeHumidity
    {
        public long low { get; set; }
        public long high { get; set; }
    }
    public class Temperature
    {
        public long low { get; set; }
        public long high { get; set; }
    }

    public class Wind
    {
        public Speed speed { get; set; }
        public string direction { get; set; }
    }
    public class Speed
    {
        public long low { get; set; }
        public long high { get; set; }
    }

    public class Time
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class Regions
    {
        public string west { get; set; }
        public string east { get; set; }
        public string central { get; set; }
        public string south { get; set; }
        public string north { get; set; }
    }

    public class API_Info
    {
        public string status { get; set; }
    }
}