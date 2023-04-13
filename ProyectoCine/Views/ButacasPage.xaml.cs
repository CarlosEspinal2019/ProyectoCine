using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButacasPage : ContentPage
    {
        public ButacasPage(Pelicula pelicula, Funcion funcion, int cantidad)
        {
            InitializeComponent();
        }

        //private async void Resumen_Clicked(object sender, SelectedItemChangedEventArgs e) 
        //{
        //    //await Navigation.PushAsync(new ButacasPage(pelicula, funcion, cant));
        //}
    }
}