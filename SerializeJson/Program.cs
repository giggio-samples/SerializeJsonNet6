using SerializeJson;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static SerializeJson.Taste;
using static System.Console;

using MemoryStream ms = new();
using Utf8JsonWriter writer = new(ms);
JsonContext.Default.Person.Serialize(writer, new Person { Name = "Giovanni" });
writer.Flush();
using StreamReader reader = new(ms);
ms.Position = 0;
WriteLine(reader.ReadToEnd());

WriteLine(JsonSerializer.Serialize(new Food { Calories = 10, Taste = Salty }, new JsonContext(new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() }
}).Food));

namespace SerializeJson
{
    internal class Person
    {
        public string Name { get; set; }
    }

    internal class Food
    {
        public Taste Taste { get; set; }
        public int Calories { get; set; }
    }

    enum Taste
    {
        Spicy, Salty, Sweet
    }

    [JsonSerializable(typeof(Person))]
    [JsonSerializable(typeof(Food))]
    internal partial class JsonContext : JsonSerializerContext
    {
    }
}
