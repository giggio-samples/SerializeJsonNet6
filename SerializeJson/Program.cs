using SerializeJson;
using System.Text.Json;
using System.Text.Json.Serialization;
using static SerializeJson.Taste;

WriteLine(JsonSerializer.Serialize(new Person("Giovanni"), JsonContext.Default.Person));
var newContext = new JsonContext(new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() }
});
WriteLine(JsonSerializer.Serialize(new Food { Calories = 10, Taste = Salty }, newContext.Food));
var person = JsonSerializer.Deserialize<Person>(@"{""Nome"":""Giovanni""}");
WriteLine(person!.Nome);

namespace SerializeJson
{
    internal record Person(string Nome);

    internal class Food
    {
        public Taste Taste { get; set; }
        public int Calories { get; set; }
    }

    enum Taste
    {
        Spicy, Salty, Sweet
    }

    [JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(Person))]
    [JsonSerializable(typeof(Food))]
    internal partial class JsonContext : JsonSerializerContext
    {
    }
}
