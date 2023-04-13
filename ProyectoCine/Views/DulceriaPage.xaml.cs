using Newtonsoft.Json;
using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DulceriaPage : ContentPage
    {
        public DulceriaPage(Pelicula pelicula, Funcion funcion, int cantidad)
        {
            InitializeComponent();
            CargarCombos();
            //calculos();
        }

        private async void CargarCombos()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
            client.BaseAddress = new Uri("https://54.162.134.64:443");

            var request = await client.GetAsync("/apiPrueba/combos.php");
            if (request.IsSuccessStatusCode)
            {
                var responseJson = await request.Content.ReadAsStringAsync();
                var listado = JsonConvert.DeserializeObject<List<Dulceria>>(responseJson);
                //listDulceria.itemSource = listado;


            }
            else
            {
                await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
            }
        }

        //private async void calculos()
        //{
        //    string cantidad = CantidadCombo1.Text;
        //    if (string.IsNullOrEmpty(cantidad))
        //    {
        //        await DisplayAlert("Validacion", "Debe ingresar la cantidad de boletos", "OK");
        //        return;
        //    }

            
        //}

    }  
}