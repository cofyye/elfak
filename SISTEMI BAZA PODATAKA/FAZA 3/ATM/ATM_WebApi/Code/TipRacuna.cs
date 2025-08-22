using System.Text.Json.Serialization;

namespace WebAPI.Code;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipRacuna
{
   devizni,
   dinarski
}