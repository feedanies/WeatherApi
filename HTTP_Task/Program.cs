using HTTP_Task.Services;

namespace HTTP_Task
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            WeatherService weatherService = new WeatherService();

            while (true)
            {
                Console.Write("\nSeherin adini daxil edin (cixmaq ucun exit): ");
                var city = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(city))
                {
                    continue;
                }
                if (city.ToLower() == "exit")
                {
                    break;
                }

                try
                {
                    var weather = await weatherService.GetWeather(city);
                    if (weather == null)
                    {
                        continue;
                    }

                    Console.WriteLine("===============================");
                    Console.WriteLine($"Seher: {weather.location.name}, {weather.location.country}");
                    Console.WriteLine($"Temperatur: {weather.current.temp_c} °C");
                    Console.WriteLine($"Hiss edilen temperatur: {weather.current.feelslike_c} °C");
                    Console.WriteLine($"Hava seraiti: {weather.current.condition.text}");
                    Console.WriteLine($"Rutubet: {weather.current.humidity} %");
                    Console.WriteLine($"Kulek: {weather.current.wind_kph} km/saat");
                    Console.WriteLine("===============================");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



    }
}

