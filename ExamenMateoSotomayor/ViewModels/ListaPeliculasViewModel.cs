using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ExamenMateoSotomayor.Models;
using ExamenMateoSotomayor.Repository;

namespace ExamenMateoSotomayor.ViewModels
{
    public class ListaPeliculasViewModel : BaseViewModel
    {
        public ObservableCollection<string> Peliculas { get; }

        public ListaPeliculasViewModel()
        {
            Peliculas = new ObservableCollection<string>();
            LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            var peliculas = await App.Database.GetMoviesAsync();
            Peliculas.Clear();

            foreach (var pelicula in peliculas)
            {
                Peliculas.Add($"Título: {pelicula.Title}, Género: {pelicula.Genre}, Actor Principal: {pelicula.Actor}, Awards: {pelicula.Awards}, Website: {pelicula.Website}, MateoSotomayor: {pelicula.MateoSotomayor}");
            }
        }
    }
}
