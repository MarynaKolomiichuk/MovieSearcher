using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace MovieSearcher
{
    public partial class Form1 : Form
    {
        private List<Film> _movies;
        private List<Film> _moviesRated = new List<Film>();
        public Form1()
        {
            InitializeComponent();
            string fileName = "jsconfig1.json";
            string filePath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, fileName);
            _movies = Film.GetFilmsFromFile(filePath);
        }

        private void search_Click(object sender, EventArgs e)
        {
            string firstUser = firstUserTitle.Text;
            string secondUser = secondUserTitle.Text;

            List<Film> highRatedFilms = new List<Film>();

            if (firstUser.Length > 3 && secondUser.Length > 3)
            {

                highRatedFilms.Add(_movies.Find(f => f.Title == firstUser));
                highRatedFilms.Add(_movies.Find(f => f.Title == secondUser));

                if (highRatedFilms[0] == null || highRatedFilms[1] == null)
                {
                    MessageBox.Show("The films was not fond!!!");
                    return;
                }

                foreach (var film in _movies)
                {
                    if (film.Title == firstUser || film.Title == secondUser) continue;

                    int firstUserGenres = film.Genre.Intersect(highRatedFilms[0].Genre).Count();
                    int secondUserGenres = film.Genre.Intersect(highRatedFilms[1].Genre).Count();
                    int firstUserActors = film.Actors.Intersect(highRatedFilms[0].Actors).Count();
                    int secondUserActors = film.Actors.Intersect(highRatedFilms[1].Actors).Count();
                    int max = Math.Max(firstUserGenres + firstUserActors, secondUserGenres + secondUserActors);
                    int min = Math.Min(firstUserGenres + secondUserActors, secondUserGenres + secondUserActors);
                    double score = min + (max - min) / 5.0;
                    film.matches = score;
                    if (film.matches > 0)
                    {
                        _moviesRated.Add(film);

                    }
                }

                var movieDetailsForm = new MovieDetailsForm();
                List<Film> sortedFilm = _moviesRated.OrderByDescending(f => f.matches).Take(20).ToList();

                movieDetailsForm.films = sortedFilm;
                movieDetailsForm.SetMovieDetails(sortedFilm[0]);
                movieDetailsForm.Show();

               

            }
            else
            {
                MessageBox.Show("Fields con`t not be empty");
            }
        }
    }
}