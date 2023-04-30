using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Windows.Forms;

namespace MovieSearcher
{
    public class Film
    {
        public double matches = 0.1;
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Actors { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ImdbLink { get; set; }
        

        public static List<Film> GetFilmsFromFile(string filePath)
        {
            List<Film> films = null;
          
            try
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };
                    var root = JsonSerializer.Deserialize<JsonElement>(json);
                    var filmsArray = root.GetProperty("films").GetRawText();
                    films = JsonSerializer.Deserialize<List<Film>>(filmsArray, options);

                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show("There was an error parsing the JSON data: " + ex.Message);
            }

            return films;
        }
    }
}
