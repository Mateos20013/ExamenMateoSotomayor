using ExamenMateoSotomayor.ViewModels;

namespace ExamenMateoSotomayor.Views
{
    public partial class ListaPeliculasPage : ContentPage
    {
        public ListaPeliculasPage()
        {
            InitializeComponent();
            BindingContext = new ListaPeliculasViewModel();
        }
    }
}
