using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using PokeApiNet.Models;


namespace PokeApiNet.Services
{
    public class PokeApiClient
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://pokeapi.co/api/v2/";

        public PokeApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<PokemonInfo>> GetTop10RandomPokemons()
        {
            HttpResponseMessage response = await _client.GetAsync("pokemon?limit=10"); // Limitando a 10 Pokémons
            List<PokemonInfo> detailedPokemons = new List<PokemonInfo>();
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var allPokemons = JsonSerializer.Deserialize<Response>(json);

            if (allPokemons?.results != null)
            {
                foreach (var pokemon in allPokemons.results)
                {
                    var detailedPokemon = await GetPokemonByName(pokemon.name); // Faz chamada para obter detalhes do pokémon
                    detailedPokemon.url = pokemon.url;
                    detailedPokemons.Add(detailedPokemon); // Adiciona os detalhes na lista
                }

                return detailedPokemons;
            }

            return new List<PokemonInfo>();
        }

        public async Task<PokemonInfo> GetPokemonByName(string name)
        {
            string lowerCaseName = name.ToLower(); // Transforma o nome em minúsculas

            HttpResponseMessage response = await _client.GetAsync($"pokemon/{HttpUtility.UrlEncode(lowerCaseName)}");

            response.EnsureSuccessStatusCode(); // Verifica se a resposta da requisição é bem-sucedida
            string json = await response.Content.ReadAsStringAsync();

            var pokemonDetails = JsonSerializer.Deserialize<PokemonInfo>(json);

            if (pokemonDetails != null)
            {
                await pokemonDetails.sprites.LoadAndConvertImageToBase64Async(); // Converte a URL da imagem front_default em base 64

                string evolutionUrl = $"https://pokeapi.co/api/v2/evolution-chain/{pokemonDetails.id}/";
                HttpResponseMessage evolutionResponse = await _client.GetAsync(evolutionUrl); // Faz uma requisição GET para a API de evolução
                evolutionResponse.EnsureSuccessStatusCode();

                string evolutionJson = await evolutionResponse.Content.ReadAsStringAsync(); // Lê o conteúdo da resposta de evolução como uma string (JSON)
                var evolutionInfo = JsonSerializer.Deserialize<object>(evolutionJson);

                if (evolutionInfo != null)
                {
                    pokemonDetails.evolution = evolutionInfo;
                }
            }

            return pokemonDetails ?? new PokemonInfo();
        }

    }
}
