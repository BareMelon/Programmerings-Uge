using System;
using System.Net.Http;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await SayHelloWorld();
    }
    private static async Task GetWeather()
    {
        var latitude = 56; // Coordinates for Denmark
        var longitude = 10;

        // Open-Meteo: current temperature only.
        var url =
            "https://api.open-meteo.com/v1/forecast"
            + $"?latitude={latitude}"
            + $"&longitude={longitude}"
            + $"&current=temperature_2m"
            + $"&timezone=Europe%2FBerlin";

        using var httpClient = new HttpClient();

        var json = await httpClient.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);
        double temp = doc.RootElement
            .GetProperty("current")
            .GetProperty("temperature_2m")
            .GetDouble();

        Console.WriteLine($"Current temperature: {temp}°C");
    }

    private static async Task SayHelloWorld()
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("What is your name?");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("No name entered!");
            return;
        }
        Console.WriteLine("Hello, " + name + "!");
        Console.WriteLine("What do you wish to do?");
        Console.WriteLine("1. Check the weather");
        string? choice = Console.ReadLine();
        if (choice == "1")
        {
            System.Console.WriteLine("Checking the weather...");
            await GetWeather();
        }
    }
}