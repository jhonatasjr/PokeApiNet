using System;
using System.Collections.Generic;

namespace PokeApiNet.Models
{
    public class PokemonInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Sprites sprites { get; set; }
        public Object evolution { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
        public string front_default_Base64 { get; set; }

        public async Task LoadAndConvertImageToBase64Async()
        {
            if (!string.IsNullOrEmpty(front_default))
            {
                using (var client = new HttpClient())
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(front_default);
                    front_default_Base64 = Convert.ToBase64String(imageBytes) ?? "";
                }
            }
        }
    }

    public class AllPokemonInfo
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<AllPokemonInfo> results { get; set; }
    }
}
