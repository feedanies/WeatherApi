using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTTP_Task.Services
{
    public class Rootobject
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
        public string localtime { get; set; }
    }

    public class Current
    {
        public float temp_c { get; set; }
        public float feelslike_c { get; set; }
        public Condition condition { get; set; }
        public int humidity { get; set; }
        public float wind_kph { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
    }

    public class WeatherService
    {
        private readonly string MyApiKey = "d3ee109866e04924af311401262710";
        private readonly string url = "http://api.weatherapi.com/v1/current.json";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<Rootobject?> GetWeather(string city)
        {
            var response = await httpClient.GetAsync($"{url}?key={MyApiKey}&q={Uri.EscapeDataString(city)}");

            Console.WriteLine($"Status code: {(int)response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode == 400)
                    Console.WriteLine("Seher tapilmadi!");
                else if ((int)response.StatusCode == 401 || (int)response.StatusCode == 403)
                    Console.WriteLine("API key yanlisdir!");
                else
                    Console.WriteLine("Serverde xeta bas verdi!");

                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Rootobject>(jsonString);

            return data;
        }
    }

}
