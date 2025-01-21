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

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public BuscarPeliculasViewModel()
        {
            SearchCommand = new Command(async () => await SearchMovieAsync());
            ClearCommand = new Command(() => SearchText = string.Empty);
        }

        private async Task SearchMovieAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Message = "Por favor ingresa un nombre de película.";
                return;
            }

            try
            {
                using HttpClient client = new();
                var response = await client.GetStringAsync($"https://freetestapi.com/api/v1/movies?search={SearchText}&limit=1");
                var movies = JsonSerializer.Deserialize<List<Pelicula>>(response);

                if (movies != null && movies.Count > 0)
                {
                    var movie = movies[0];
                    movie.MateoSotomayor = "Mateo Sotomayor";
                    await App.Database.SaveMovieAsync(movie);
                    Message = "Película guardada exitosamente.";
                }
                else
                {
                    Message = "No se encontró ninguna película.";
                }
            }
            catch
            {
                Message = "Hubo un error al buscar la película.";
            }
        }
    }
}
