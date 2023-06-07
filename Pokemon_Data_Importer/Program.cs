using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Pokemon_Data_Importer;

//This version attempts to load the data from a file. If the file is not found one is created and filled with the data from PokeAPI
try
{
    //File where the all of the data is stored
    string pokemonDataFile = "pokemon_data.json";

    // Check if the data file exists
    if (File.Exists(pokemonDataFile))
    {
        // Load the data from the file
        string jsonData = File.ReadAllText(pokemonDataFile);
        List<Pokemon> pokemonList = JsonConvert.DeserializeObject<List<Pokemon>>(jsonData);

        // Prompt user to enter a Pokemon name
        Console.Write("Enter a Pokemon name: ");
        string pokemonName = Console.ReadLine();

        // Find the specific Pokemon in the loaded data
        Pokemon pokemon = pokemonList.Find(p => p.Name.Equals(pokemonName, StringComparison.OrdinalIgnoreCase));

        if (pokemon != null)
        {
            // Display the Pokemon information
            DisplayPokemonInformation(pokemon);
        }
        else
        {
            Console.WriteLine("Pokemon not found in the data.");
        }
    }
    else
    {
        // Fetch data for every Pokemon
        List<Pokemon> pokemonList = new List<Pokemon>();
        for (int i = 1; i <= 1010; i++) // Total number of Pokemon is 898 as of September 2021
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{i}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(jsonResponse);
                pokemonList.Add(pokemon);
                Console.WriteLine($"Fetched data for {pokemon.Name}");
            }
            else
            {
                Console.WriteLine($"Failed to fetch data for Pokemon with ID {i}. Status code: {response.StatusCode}");
            }
        }

        // Save the data to a file
        string jsonData = JsonConvert.SerializeObject(pokemonList, Formatting.Indented);
        File.WriteAllText(pokemonDataFile, jsonData);

        Console.WriteLine("Data saved to the file.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}

//Givena Pokemon object, output the data fields for that object
static void DisplayPokemonInformation(Pokemon pokemon)
{
    Console.WriteLine("Pokemon Information:");
    Console.WriteLine($"Name: {pokemon.Name}");
    Console.WriteLine($"ID: {pokemon.Id}");
    Console.WriteLine($"Order: {pokemon.Order}");
    Console.WriteLine($"Base Experience: {pokemon.BaseExperience}");
    Console.WriteLine($"Height: {pokemon.Height}");
    Console.WriteLine($"Weight: {pokemon.Weight}");
    Console.WriteLine("Types:");
    foreach (PokemonTypes type in pokemon.Types)
    {
        Console.WriteLine($"- {type.Type.Name}");
    }
    Console.WriteLine("Abilities:");
    foreach (PokemonAbility ability in pokemon.Abilities)
    {
        Console.WriteLine($"- {ability.Ability.Name} (Hidden: {ability.IsHidden})");
    }
    Console.WriteLine("Stats:");
    foreach (PokemonStats stat in pokemon.Stats)
    {
        Console.WriteLine($"- {stat.Stat.Name}: Base {stat.BaseStat}, Effort {stat.Effort}");
    }
}