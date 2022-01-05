namespace BlazorDemo.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public class WeatherItem
    {
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }

        public string Period { get; set; }

        public string SymbolNumber { get; set; }
        public string SymbolName { get; set; }
        public string SymbolVar { get; set; }

        public string PrecipitationValue { get; set; }

        public string WindDirectionDeg { get; set; }
        public string WindDirectionCode { get; set; }
        public string WindDirectionName { get; set; }
        public string WindSpeedMPS { get; set; }
        public string WindSpeedName { get; set; }

        public string Temp { get; set; }
        public string Pressure { get; set; }

        public bool IsFilled { get; set; }

    }

    public class WeatherData
    {
        public string LocationName { get; set; }
        public string LocationType { get; set; }
        public string LocationCountry { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }

        public string SunRise { get; set; }
        public string SunSet { get; set; }

        public List<WeatherItem> WeatherItems { get; set; }

        public bool IsFilled { get; set; }

        public  string  Credit { get; set; }

        public WeatherData()
        {
            WeatherItems = new List<WeatherItem>();
        }
    }
}