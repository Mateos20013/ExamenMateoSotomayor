using SQLite;
using ExamenMateoSotomayor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenMateoSotomayor.Repository
{
    public class DatabaseRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            
            _database.CreateTableAsync<Pelicula>().Wait();
        }

        
        public Task<int> SaveMovieAsync(Pelicula pelicula)
        {
            return _database.InsertAsync(pelicula);
        }

        
        public Task<List<Pelicula>> GetMoviesAsync()
        {
            return _database.Table<Pelicula>().ToListAsync();
        }

        
        public Task<int> DeleteAllMoviesAsync()
        {
            return _database.DeleteAllAsync<Pelicula>();
        }
    }
}
