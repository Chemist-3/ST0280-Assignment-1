<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeatherService.aspx.cs" Inherits="WeatherServiceWebApp.WeatherService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Singapore's 24-hour forecast</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.26.0/moment-with-locales.min.js"></script>
    <script>

        let urlAPI = "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=";
        let datetimeNow = moment().format();

        $('#generateForm').submit(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.

            $.ajax({
                method: 'GET',
                url: urlAPI + encodeURIComponent(datetimeNow),
            }).done(function (data) {
                // Do something with data
                console.log("Sucess! datetime: " + datetimeNow);
            }).fail(function (data) {
                console.log("Error! datetime: " + datetimeNow);
            })

        })

    </script>
</head>
<body>
    <form id="generateForm">


    </form>
    <div id="display forecast">

    </div>
    </body>
</html>
