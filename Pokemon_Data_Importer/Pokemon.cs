using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Pokemon_Data_Importer
{
	public class Pokemon
	{
        [JsonProperty("name")]
        public string? Name { get; set; }

        //Likely references national Pokedex number?
        [JsonProperty("id")]
        public int? Id { get; set; }

        //Order is almost national Pokedex order, but families are grouped together
        [JsonProperty("order")]
        public int? Order { get; set; }

        [JsonProperty("base_experience")]
        public int? BaseExperience { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("weight")]
        public int? Weight { get; set; }

        [JsonProperty("types")]
        public List<PokemonTypes>? Types { get; set; }

        [JsonProperty("abilities")]
        public List<PokemonAbility>? Abilities { get; set; }

        [JsonProperty("stats")]
        public List<PokemonStats>? Stats { get; set; }
    }

    //Classes to hold info on the different Pokemon types
    public class PokemonTypes
    {
        [JsonProperty("slot")]
        public int? Slot { get; set; }

        [JsonProperty("type")]
        public TypeInfo? Type { get; set; }
    }

    public class TypeInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    //Classes to hold info on the different Pokemon abilities
    public class PokemonAbility
    {
        [JsonProperty("ability")]
        public AbilityInfo? Ability { get; set; }

        [JsonProperty("is_hidden")]
        public bool? IsHidden { get; set; }

        [JsonProperty("slot")]
        public int? Slot { get; set; }
    }

    public class AbilityInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    //Classes to hold info on the different Pokemon stats
    public class PokemonStats
    {
        [JsonProperty("base_stat")]
        public int? BaseStat { get; set; }

        [JsonProperty("effort")]
        public int? Effort { get; set; }

        [JsonProperty("stat")]
        public StatInfo? Stat { get; set; }
    }

    public class StatInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

}

