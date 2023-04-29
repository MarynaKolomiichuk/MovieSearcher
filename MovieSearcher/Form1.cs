using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MovieSearcher
{
    public partial class Form1 : Form
    {
        private List<Film> _movies;
        private List<Film> _moviesRated = new List<Film>();
        public Form1()
        {
            InitializeComponent();
            _movies = Film.GetFilmsFromFile("C:\\Users\\Vanish\\Desktop\\vlad\\MovieSearcher\\MovieSearcher\\jsconfig1.json");
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
                    int secondUserGenres =  film.Genre.Intersect(highRatedFilms[1].Genre).Count();
                    int firstUserActors= film.Genre.Intersect(highRatedFilms[0].Actors).Count();
                    int secondUserActors = film.Genre.Intersect(highRatedFilms[1].Actors).Count();
                    int max = Math.Max(firstUserGenres + firstUserActors, secondUserGenres + secondUserActors);
                    int min = Math.Min(firstUserGenres + secondUserActors, secondUserGenres + secondUserActors);
                    film.matches = min + (max - min) / 5;
                    if (film.matches > 4) MessageBox.Show("More than 4");
                    if (film.matches > 0)
                    {
                        _moviesRated.Add(film);

                    }
                }        
                    

                var movieDetailsForm = new MovieDetailsForm();
                movieDetailsForm.films = _moviesRated.OrderByDescending(f => f.matches).ToList(); ;
                movieDetailsForm.SetMovieDetails(_moviesRated[0]);
                movieDetailsForm.Show();

               

            }
            else
            {
                MessageBox.Show("Fields con`t not be empty");
            }
            listView1.View = View.Details;
            listView1.Columns.Add("Title", 300);
        }
    }
}