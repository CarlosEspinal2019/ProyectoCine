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
		public CarteleraPage ()
		{
			InitializeComponent ();
			CargarPeliculas();
		}

        private async void CargarPeliculas()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
			client.BaseAddress = new Uri("https://44.202.96.176:443");

			var request = await client.GetAsync("/apiC");
			if (request.IsSuccessStatusCode) 
			{
				var responseJson = await request.Content.ReadAsStringAsync();
				var listado = JsonConvert.DeserializeObject<List<Pelicula>>(responseJson);
				listPeliculas.ItemsSource = listado;
			}
			else
			{
				await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
			}
        }

		private async void Pelicula_Selected (object sender, SelectedItemChangedEventArgs e) 
		{
			var pelicula = (Pelicula)e.SelectedItem;
			await Navigation.PushAsync(new FuncionesPage(pelicula));
		}
    }
}