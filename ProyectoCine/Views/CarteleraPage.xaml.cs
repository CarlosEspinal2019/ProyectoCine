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
	public partial class CarteleraPage : ContentPage
	{
        private Pelicula pelicula;
        private int idCine;

        public CarteleraPage (int idCine)
		{
			InitializeComponent ();
            this.idCine = idCine;
			CargarPeliculas(idCine);
		}

        private async Task<string> CargarPeliculas( int idCine)
        {
            //         var httpClientHandler = new HttpClientHandler();

            //         httpClientHandler.ServerCertificateCustomValidationCallback =
            //         (message, cert, chain, errors) => { return true; };
            //         HttpClient client = new HttpClient(httpClientHandler);
            //client.BaseAddress = new Uri("https://54.162.134.64:443");

            //var request = await client.GetAsync("/apiPrueba");
            //if (request.IsSuccessStatusCode) 
            //{
            //	var responseJson = await request.Content.ReadAsStringAsync();
            //	var listado = JsonConvert.DeserializeObject<List<Pelicula>>(responseJson);                
            //             listPeliculas.ItemsSource = listado;


            //         }
            //else
            //{
            //	await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
            //}

            //SEGUNDO METODO

            string apiUrl = "https://54.162.134.64/apiPrueba/index.php?id="; // Reemplaza esta URL con la URL de tu API
            string url = apiUrl + idCine; // Agrega el parámetro ID en la URL

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
                    var listado = JsonConvert.DeserializeObject<List<Pelicula>>(responseBody);
                    listPeliculas.ItemsSource = listado;
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

		private async void Pelicula_Selected (object sender, SelectedItemChangedEventArgs e) 
		{
            
            var pelicula = (Pelicula)e.SelectedItem;
			await Navigation.PushAsync(new FuncionesPage(pelicula, idCine));
		}
    }
}