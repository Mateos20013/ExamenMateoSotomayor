using SQLite;

namespace ExamenMateoSotomayor.Models
{
    public class Pelicula
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
        public string Descripcion { get; set; }
    }
}
