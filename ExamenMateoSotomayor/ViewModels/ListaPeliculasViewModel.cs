using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ExamenMateoSotomayor.Models;
using ExamenMateoSotomayor.Repository;

namespace ExamenMateoSotomayor.ViewModels
{
    public class ListaPeliculasViewModel : BaseViewModel
    {
        public ObservableCollection<Pelicula> Peliculas { get; }

        public ListaPeliculasViewModel()
        {
            Peliculas = new ObservableCollection<Pelicula>();
            LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            var peliculas = await App.Database.GetMoviesAsync();
            Peliculas.Clear();
            foreach (var pelicula in peliculas)
            {
                Peliculas.Add(pelicula);
            }
        }
    }
}
