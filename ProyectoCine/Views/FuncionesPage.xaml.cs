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
            string idPelicula = pelicula.idPelicula;
			BindingContext = pelicula;
            CargarHorarios(idPelicula);

        }

        private async Task<string> CargarHorarios( string idPelicula )
        {
            //var httpClientHandler = new HttpClientHandler();

            //httpClientHandler.ServerCertificateCustomValidationCallback =
            //(message, cert, chain, errors) => { return true; };
            //HttpClient client = new HttpClient(httpClientHandler);
            //client.BaseAddress = new Uri("https://54.162.134.64:443");

            //var request = await client.GetAsync("/apiPrueba/Funcion.php");
            //if (request.IsSuccessStatusCode)
            //{
            //    var responseJson = await request.Content.ReadAsStringAsync();
            //    var listado = JsonConvert.DeserializeObject<List<Funcion>>(responseJson);
            //    listFunciones.ItemsSource = listado;


            //}
            //else
            //{
            //    await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
            //}

            
            string apiUrl = "https://54.162.134.64/apiPrueba/Funcion.php?id="; // Reemplaza esta URL con la URL de tu API
            string url = apiUrl + idPelicula; // Agrega el parámetro ID en la URL

            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
                HttpClient client = new HttpClient(httpClientHandler);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var listado = JsonConvert.DeserializeObject<List<Funcion>>(responseBody);
                    listFunciones.ItemsSource = listado;
                    return responseBody;
                }
                else
                {
                    // Maneja el caso cuando la respuesta no es exitosa
                    throw new Exception("Error en la solicitud: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que ocurra durante la solicitud
                throw new Exception("Error al realizar la solicitud: " + ex.Message);
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
            //await Navigation.PushAsync(new ButacasPage(pelicula, funcion, cant));
            await Navigation.PushAsync(new DulceriaPage(pelicula, funcion, cant));
        }
	}
}