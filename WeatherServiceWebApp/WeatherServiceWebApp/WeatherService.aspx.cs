using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherServiceWebApp.Model;

namespace WeatherServiceWebApp
{
    public partial class WeatherService : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // API and DateTime query
            string webAPI = "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=";
            string query = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz");
            

            // Create Request for API and current datetime as query, URL encoded
            WebRequest request = WebRequest.Create(webAPI + HttpUtility.UrlEncode(query));
            request.Timeout = 15 * 1000;
            request.ContentType = "application/json";


            try
            {
                // Get the response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // JSON Deserialize
                var model = JsonConvert.DeserializeObject<RootObject>(responseFromServer);

                // Display the content.
                DateTime startDate = DateTime.Parse(model.items[0].valid_period.start);
                DateTime endDate = DateTime.Parse(model.items[0].valid_period.end);

                Response.Write("<h1>Singapore's 24-hour Forecast from " + startDate + " to " + endDate + "</h1>");
                Response.Write(String.Format("<br> Forecast: {0} <br> Temperature: Lowest {1}°C, Highest {2}°C", model.items[0].general.forecast, model.items[0].general.temperature.low, model.items[0].general.temperature.high));
                Response.Write(String.Format("<br> Relative Humidity: {0}% - {1}% <br> Wind: {2} {3} - {4} km/h", model.items[0].general.relative_humidity.low, model.items[0].general.relative_humidity.high, model.items[0].general.wind.direction, model.items[0].general.wind.speed.low, model.items[0].general.wind.speed.high));
                Response.Write("<br><br>Please note that the temperature, relative humidity and wind information shown above are the respective forecasts over a 24-hour period.");
                


                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                // Error handling
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Response.Write("Error accessing web service. Check console for more infomation!");
            }
        }
    }
}