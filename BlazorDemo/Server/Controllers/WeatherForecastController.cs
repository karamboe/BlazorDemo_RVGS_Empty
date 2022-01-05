using BlazorDemo.Shared;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text;
using System.Xml;

namespace BlazorDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public WeatherData Get()
        {
            //return Enumerable.Range(1, 15).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            return GetWeatherData();
            
        }

        string url = "https://www.yr.no/place/Norge/M%C3%B8re%20og%20Romsdal/Molde/Molde/forecast.xml"; // http://www.yr.no/place/Sweden/Stockholm/J%c3%a4rva/forecast.xml";

        private string GetWeatherXml()
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            return client.Execute(request).Content;
        }

        private WeatherData GetWeatherData()
        {
            WeatherData Data = new WeatherData() { WeatherItems = new List<WeatherItem>() };

            StringBuilder sb = new StringBuilder("");

            System.Xml.XmlDocument doc = new XmlDocument();

            doc.LoadXml(GetWeatherXml());

            System.Xml.XmlNode sun = doc.SelectSingleNode("/weatherdata/sun");
            System.Xml.XmlNode location = doc.SelectSingleNode("/weatherdata/location");
            System.Xml.XmlNodeList nodelist = doc.SelectNodes("/weatherdata/forecast/tabular/time");
            System.Xml.XmlNode credit = doc.SelectSingleNode("/weatherdata/credit");

            Data.SunRise = sun.Attributes["rise"].Value;
            Data.SunSet = sun.Attributes["set"].Value;
            Data.IsFilled = true;

            Data.LocationName = location["name"].Value;
            Data.LocationType = location["type"].Value;
            Data.LocationCountry = location["country"].Value;

            Data.LocationLatitude = location["location"].Attributes["latitude"].Value;
            Data.LocationLongitude = location["location"].Attributes["longitude"].Value;
            
            Data.Credit = credit["link"].Attributes["text"].Value;

            for (int i = 0; i < 7; i++)
            {
                WeatherItem item = new WeatherItem();

                item.Period = nodelist[i].Attributes["period"].Value;
                item.TimeFrom = DateTime.Parse(nodelist[i].Attributes["from"].Value).ToString("yyyy-MM-dd HH:mm");
                item.TimeTo = DateTime.Parse(nodelist[i].Attributes["to"].Value).ToString("yyyy-MM-dd HH:mm");
                item.SymbolNumber = nodelist[i]["symbol"].Attributes["number"].Value.PadLeft(2, '0');
                item.SymbolName = nodelist[i]["symbol"].Attributes["name"].Value;
                item.Temp = nodelist[i]["temperature"].Attributes["value"].Value + "C";
                item.PrecipitationValue = nodelist[i]["precipitation"].Attributes["value"].Value;
                item.WindSpeedMPS = nodelist[i]["windSpeed"].Attributes["mps"].Value;
                item.WindSpeedName = nodelist[i]["windSpeed"].Attributes["name"].Value;
                item.WindDirectionCode = nodelist[i]["windDirection"].Attributes["code"].Value;
                item.WindDirectionDeg = nodelist[i]["windDirection"].Attributes["deg"].Value;
                item.WindDirectionName = nodelist[i]["windDirection"].Attributes["name"].Value;
                item.Pressure = nodelist[i]["pressure"].Attributes["value"].Value + " " + nodelist[i]["pressure"].Attributes["unit"].Value;

                item.IsFilled = true;

                Data.WeatherItems.Add(item);
            }

            return Data;
        }

    }
}