using System.Text.Json.Serialization;
using API.Converters;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class JsonOptionsExceptions
    {
        public static void AddConverters(this JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
        }
    }
}