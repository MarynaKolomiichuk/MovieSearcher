using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieSearcher
{
    public partial class MovieDetailsForm : Form
    {
        public int page = 0;
        public List<Film> films = new List<Film>();

        public MovieDetailsForm()
        {
            InitializeComponent();
        }

        public void SetMovieDetails(Film movie)
        {
            // Set the text of the labels and picture boxes to display the details of the movie
            titleLabel.Text = movie.Title;
            directorLabel.Text = movie.Director;
            yearLabel.Text = movie.Year.ToString();
            actorsLabel.Text = string.Join(", ", movie.Actors);
            genreLabel.Text = string.Join(", ", movie.Genre);
            pictureBox1.ImageLocation = findIMG(movie.Title);
            descriptionTextBox.Text = "    " + movie.Description;
            label1.Text = movie.matches.ToString();
        }
        private string findIMG(string title)
        {
            string modifiedString = title.Replace(" ", "+");
            string url = "http://www.omdbapi.com/?t=" + modifiedString + "&apikey=e17f08db";
            using (WebClient wc = new WebClient() { Encoding = Encoding.UTF8 })
            {
                var json = wc.DownloadString(url);
                var result = JsonConvert.DeserializeObject<ImdbEntity>(json);

                if (result.Response == "True")
                {
                    return result.Poster;
                }
                else
                {
                    return "";
                }

            }

        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page -= 1;
                SetMovieDetails(films[page]);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (page < films.Count - 1) {
                page += 1;
                SetMovieDetails(films[page]);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void watch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(films[page].ImdbLink);
        }
    }


}
