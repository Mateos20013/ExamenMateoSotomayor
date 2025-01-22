using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenMateoSotomayor.Models;
using ExamenMateoSotomayor.Repository;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExamenMateoSotomayor.ViewModels
{
    public partial class ListaPeliculasViewModel : ObservableObject
    {
        private readonly DatabaseRepository _databaseRepository;

        [ObservableProperty]
        private ObservableCollection<Pelicula> peliculas;

        public ListaPeliculasViewModel()
        {
            _databaseRepository = App.Database;
            Peliculas = new ObservableCollection<Pelicula>();
            _ = CargarPeliculasAsync();
        }

        private async Task CargarPeliculasAsync()
        {
            var listaPeliculas = await _databaseRepository.GetMoviesAsync();
            Peliculas.Clear();

            foreach (var pelicula in listaPeliculas)
            {
                Peliculas.Add(new Pelicula
                {
                    Title = pelicula.Title,
                    Genre = pelicula.Genre,
                    Actors = pelicula.Actors,
                    Awards = pelicula.Awards,
                    Website = pelicula.Website
                });
            }
        }

        [RelayCommand]
        public async Task CambiarPestaña()
        {
            await Shell.Current.GoToAsync("//Buscador");
        }
    }
}
