using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {
            List<Film> films = Film.GetFilmsFromFile("C:\\Users\\vnata\\Desktop\\Норвегия\\C#\\As4\\MovieSearcher\\MovieSearcher\\MovieSearcher\\jsconfig1.json");

            string firstUser = firstUserTitle.Text;
            string secondUser = secondUserTitle.Text;

            List<Film> highRatedFilms;
            List<string> genre = new List<string>();


            if (firstUser.Length > 3 && secondUser.Length > 3)
            {
                highRatedFilms = films.FindAll(f => f.Title == firstUser || f.Title == secondUser);

                foreach (Film film in highRatedFilms)
                {
                    foreach (string item in film.Genre)
                        genre.Add(item);
                }
                foreach (string item in genre)
                {
                    ListViewItem item1 = new ListViewItem(item);
                    listView1.Items.Add(item1);
                }
            } else
            {
                MessageBox.Show("Fields con`t not be empty");
            }
 

            listView1.View = View.Details;
            listView1.Columns.Add("Title", 300);

           // foreach (Film film in films)
           // {
           //     ListViewItem item = new ListViewItem(film.Title);
           //     listView1.Items.Add(item);
            //}
        }
    }
}
