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
                // TODO
                Response.Write("Timestamp: " + model.items[0].timestamp + "<br>");
                Response.Write("API Status: " + model.api_info.status + "<br>");
                


                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine(ex.Message);
                Response.Write("Error accessing web service. Check console for more infomation!");
            }
        }
    }
}