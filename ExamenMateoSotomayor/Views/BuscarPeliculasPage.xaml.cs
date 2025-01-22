using ExamenMateoSotomayor.ViewModels;

namespace ExamenMateoSotomayor.Views
{
    public partial class BuscarPeliculasPage : ContentPage
    {
        public BuscarPeliculasPage()
        {
            InitializeComponent();
            BindingContext = new BuscarPeliculasViewModel();
        }
    }
}
