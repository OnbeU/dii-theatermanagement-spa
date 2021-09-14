﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FakeTheaterBff.Data
{
    // Note that much of this code was generated by https://app.quicktype.io/?l=csharp

    /// <summary>
    /// Information about a movie.
    /// Can be instantiated from JSON data obtained at http://omdbapi.com/ like this:
    /// 
    ///    var movieMetadata = MovieMetadata.FromJson(jsonString);
    /// 
    /// </summary>

    public partial class MovieMetadata
    {
        [JsonIgnore]
        public long MovieMetadataId { get; set; }

        [JsonProperty("Title")]
        [Required]
        public string Title { get; set; }

        [JsonProperty("imdbID")]
        [MaxLength(32)]
        public string ImdbId { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Rated")]
        public string Rated { get; set; }

        [JsonProperty("Released")]
        public string Released { get; set; }

        [JsonProperty("Runtime")]
        public string Runtime { get; set; }

        [JsonProperty("Genre")]
        public string Genre { get; set; }

        [JsonProperty("Director")]
        public string Director { get; set; }

        [JsonProperty("Writer")]
        public string Writer { get; set; }

        [JsonProperty("Actors")]
        public string Actors { get; set; }

        [JsonProperty("Plot")]
        public string Plot { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Awards")]
        public string Awards { get; set; }

        [JsonProperty("Poster")]
        public Uri Poster { get; set; }

        // Note that we removed the Ratings array because it was too complicated to make work with EF Core.

        [JsonProperty("Metascore")]
        public string Metascore { get; set; }

        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }

        [JsonProperty("imdbVotes")]
        public string ImdbVotes { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("totalSeasons")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotalSeasons { get; set; }

        [JsonProperty("Response")]
        public string Response { get; set; }
    }

    public partial class MovieMetadata
    {
        public static MovieMetadata FromJson(string json) => JsonConvert.DeserializeObject<MovieMetadata>(json, FakeTheaterBff.Data.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MovieMetadata self) => JsonConvert.SerializeObject(self, FakeTheaterBff.Data.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
