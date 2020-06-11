<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeatherService.aspx.cs" Inherits="WeatherServiceWebApp.WeatherService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Singapore's 24-hour forecast</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.26.0/moment-with-locales.min.js"></script>
    <script>
        // API
        let urlAPI = "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=";
        // Get datetime using moment.js
        let datetimeNow = moment().format();

        function ajaxCall() {
            $.ajax({
                method: 'GET',
                url: urlAPI + encodeURIComponent(datetimeNow),
            }).done(function (data) {
                // Append weather infomation
                $('#display_forecast h1').append("Singapore's 24-hour Forecast from " + moment(data.items[0].valid_period.start).format('DD/M/YYYY h:mm:ss A') + " to " + moment(data.items[0].valid_period.end).format('DD/M/YYYY h:mm:ss A') + " (Client JS)");
                $('#forecast').append("Forecast: " + data.items[0].general.forecast);
                $('#temp').append("Temperature: Lowest " + data.items[0].general.temperature.low + "°C, ");
                $('#temp').append("Highest " + data.items[0].general.temperature.high + "°C");
                $('#RH').append("Relative Humidity: " + data.items[0].general.relative_humidity.low + "% - ");
                $('#RH').append(data.items[0].general.relative_humidity.high + "%");
                $('#wind').append("Wind: " + data.items[0].general.wind.direction + " " + data.items[0].general.wind.speed.low + " - " + data.items[0].general.wind.speed.high + " km/h");

                // To show in browser client
                console.log("Success!");

            }).fail(function (data) {
                // Append to show web service error
                $('#display_forecast h1').append("Error accessing web service. Check console for more infomation!");
                $('#display_forecast p').append("API Status: " + data.api_info.status);

                // To show in browser client
                console.log("Error!");

            })
        }
    </script>
</head>
<body>
    <button onclick="ajaxCall()">Click me for Javascript Weather Service!</button>
    <div id="display_forecast">
        <h1></h1>
        <p></p>
        <p id="forecast"></p>
        <p id="temp"></p>
        <p id="RH"></p>
        <p id="wind"></p>
    </div>
</body>
</html>
