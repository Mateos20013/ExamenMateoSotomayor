using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ExamenMateoSotomayor.Models;
using ExamenMateoSotomayor.Repository;

namespace ExamenMateoSotomayor.ViewModels
{
    public class BuscarPeliculasViewModel : BaseViewModel
    {
        private string _searchText;
        private string _message;
        private string _movieDetails;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string MovieDetails
        {
            get => _movieDetails;
            set => SetProperty(ref _movieDetails, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public BuscarPeliculasViewModel()
        {
            SearchCommand = new Command(async () => await SearchMovieAsync());
            ClearCommand = new Command(() =>
            {
                SearchText = string.Empty;
                Message = string.Empty;
                MovieDetails = string.Empty;
            });
        }

        private async Task SearchMovieAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Message = "Por favor ingresa un nombre de película.";
                MovieDetails = string.Empty;
                return;
            }

            try
            {
                using HttpClient client = new();
                var response = await client.GetStringAsync($"https://freetestapi.com/api/v1/movies?search={SearchText}&limit=1");
                var peliculas = JsonSerializer.Deserialize<List<Pelicula>>(response);

                if (peliculas != null && peliculas.Count > 0)
                {
                    var pelicula = peliculas[0];
                    var nuevaPelicula = new Pelicula
                    {
                        Title = pelicula.Title,
                        Genre = pelicula.Genre != null && pelicula.Genre.Count() > 0
                            ? string.Join(", ", pelicula.Genre)
                            : "Sin género",
                        Actors = pelicula.Actors != null && pelicula.Actors.Count() > 0
                            ? string.Join(", ", pelicula.Actors)
                            : "Sin actor principal",
                        Awards = pelicula.Awards ?? "Sin premios",
                        Website = pelicula.Website ?? "Sin sitio web",
                        MateoSotomayor = "Mateo Sotomayor"
                    };

                    await App.Database.SaveMovieAsync(nuevaPelicula);
                    Message = "Película guardada exitosamente.";

                    MovieDetails = $"Título: {nuevaPelicula.Title}\n" +
                                   $"Género: {nuevaPelicula.Genre}\n" +
                                   $"Actor Principal: {nuevaPelicula.Actors}\n" +
                                   $"Premios: {nuevaPelicula.Awards}\n" +
                                   $"Sitio Web: {nuevaPelicula.Website}";
                }
                else
                {
                    Message = "No se encontró ninguna película.";
                    MovieDetails = string.Empty;
                }
            }
            catch
            {
                Message = "Hubo un error al buscar la película.";
                MovieDetails = string.Empty;
            }
        }
    }
}
