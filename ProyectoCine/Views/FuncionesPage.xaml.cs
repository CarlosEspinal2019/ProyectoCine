using Newtonsoft.Json;
using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FuncionesPage : ContentPage
	{
		private Pelicula pelicula;
		public FuncionesPage (Pelicula pelicula)
		{
			InitializeComponent ();
			this.pelicula = pelicula;
			BindingContext = pelicula;
            CargarHorarios();

        }

        private async void CargarHorarios( )
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
            client.BaseAddress = new Uri("https://54.162.134.64:443");

            var request = await client.GetAsync("/apiPrueba/Funcion.php");
            if (request.IsSuccessStatusCode)
            {
                var responseJson = await request.Content.ReadAsStringAsync();
                var listado = JsonConvert.DeserializeObject<List<Funcion>>(responseJson);
                listFunciones.ItemsSource = listado;


            }
            else
            {
                await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
            }
        }

        private async void Funcion_Selected (object sender, SelectedItemChangedEventArgs e)
		{
			string cantidad = CantidadBoletos.Text;
			if (string.IsNullOrEmpty(cantidad))
			{
				await DisplayAlert("Validacion", "Debe ingresar la cantidad de boletos", "OK");
				return;
			}

			int cant = Convert.ToInt32(cantidad);
			var funcion = (Funcion)e.SelectedItem;

            //await Navigation.PushAsync(new ResumenPage(pelicula, funcion, cant));
            await Navigation.PushAsync(new ButacasPage(pelicula, funcion, cant));
        }
	}
}