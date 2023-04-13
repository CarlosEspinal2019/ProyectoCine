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
    public partial class SeleccionCiudadPage : ContentPage
    {
        private Pelicula pelicula;
        public SeleccionCiudadPage()
        {
            InitializeComponent();
        }

        // C#
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int  selectedItem = picker.SelectedIndex;

            int idCine = selectedItem;
            if (selectedItem ==1)
            {                
                Navigation.PushAsync(new CarteleraPage(idCine));
            }
            if (selectedItem == 2)
            {

                Navigation.PushAsync(new CarteleraPage(idCine));
            }

            else 
            {
                //DisplayAlert("Debe seleccionar ciudad", "Datos invalidos", "Aceptar");
            }
        }

    }
}