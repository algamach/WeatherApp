using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class WeatherData
    {
        public MainInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public WindInfo Wind { get; set; }
    }
    public class MainInfo
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
    }

    public class WindInfo
    {
        public float Speed { get; set; }
    }
}
