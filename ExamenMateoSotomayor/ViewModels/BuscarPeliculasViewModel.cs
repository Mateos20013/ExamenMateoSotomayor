using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenMateoSotomayor.Models;
using ExamenMateoSotomayor.Repository;

namespace ExamenMateoSotomayor.ViewModels
{
    public partial class BuscarPeliculasViewModel : ObservableObject
    {
        private readonly DatabaseRepository _databaseRepository;

        [ObservableProperty]
        private string tituloPelicula;

        [ObservableProperty]
        private string resultadoBusqueda;

        public BuscarPeliculasViewModel()
        {
            _databaseRepository = App.Database;
        }

        [RelayCommand]
        public void LimpiarCampos()
        {
            TituloPelicula = string.Empty;
            ResultadoBusqueda = string.Empty;
        }

        [RelayCommand]
        public async Task BuscarPeliculaAsync()
        {
            if (string.IsNullOrWhiteSpace(TituloPelicula))
            {
                ResultadoBusqueda = "Por favor, ingresa un nombre de una película.";
                return;
            }

            try
            {
                var httpClient = new HttpClient();
                var url = $"https://www.freetestapi.com/api/v1/movies?search={TituloPelicula}";

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var peliculas = JsonSerializer.Deserialize<List<JsonElement>>(jsonResponse);

                    if (peliculas != null && peliculas.Count > 0)
                    {
                        var pelicula = peliculas[0];

                        var titulo = pelicula.GetProperty("title").GetString();
                        var genero = string.Join(", ", pelicula.GetProperty("genre").EnumerateArray().Select(g => g.GetString()));
                        var actorPrincipal = string.Join(", ", pelicula.GetProperty("actors").EnumerateArray().Select(a => a.GetString()));
                        var awards = pelicula.GetProperty("awards").GetString();
                        var website = pelicula.GetProperty("website").GetString();

                        var nuevaPelicula = new Pelicula
                        {
                            Title = titulo ?? "No disponible",
                            Genre = genero ?? "No disponible",
                            Actors = actorPrincipal ?? "No disponible",
                            Awards = awards ?? "No disponible",
                            Website = website ?? "No disponible"
                        };

                        // Insertar la película en la base de datos
                        await _databaseRepository.SaveMovieAsync(nuevaPelicula);

                        ResultadoBusqueda = $"Película: {nuevaPelicula.Title}\n" +
                                            $"Género: {nuevaPelicula.Genre}\n" +
                                            $"Actor Principal: {nuevaPelicula.Actors}\n" +
                                            $"Premios: {nuevaPelicula.Awards}\n" +
                                            $"Sitio Web: {nuevaPelicula.Website}\n" +
                                            $"Nombre: Mateo Sotomayor";
                    }
                    else
                    {
                        ResultadoBusqueda = "No se encontró ninguna película con ese nombre.";
                    }
                }
                else
                {
                    ResultadoBusqueda = $"Error: {response.StatusCode}. No se pudo buscar la película.";
                }
            }
            catch (Exception ex)
            {
                ResultadoBusqueda = $"Error: {ex.Message}. Verifica tu conexión a internet.";
            }
        }

        [RelayCommand]
        public async Task CambiarPestaña()
        {
            await Shell.Current.GoToAsync("//Listado");
        }
    }
}
