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
            // API (Static)
            WebRequest request = WebRequest.Create("https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=2020-06-08T06%3A00%3A00%2B08%3A00");
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
                var model = JsonConvert.DeserializeObject<RootObject>(responseFromServer);

                // Display the content.
                // TODO
                Response.Write("Timestamp: " + model.items[0].timestamp + "<br>");
                Response.Write("API Status: " + model.api_info.status);


                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}